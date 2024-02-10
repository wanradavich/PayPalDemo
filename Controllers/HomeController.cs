using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PayPalDemo.Models;
using PayPalDemo.Repositories;
using PayPalDemo.ViewModels;

namespace PayPalDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, IConfiguration configuration)
        {
            _logger = logger;
            _context = context;
            _configuration = configuration; 
        }

        // Home page shows list of items.
        // Item price is set through the ViewBag.
        // Home page shows list of items.
        // Item price is set through the ViewBag.
        public IActionResult Index()
        {

            string customerName = "";
            customerName = _context.MyRegisteredUsers.FirstOrDefault(c => c.Email == User.Identity.Name)?.FirstName;
            if (customerName == null)
            {
                customerName = "";
            }
            HttpContext.Session.SetString("SessionUserName", customerName);

            var siteKey = _configuration["Recaptcha:SiteKey"];
            var secretKey = _configuration["Recaptcha:SecretKey"];
            var clientId = _configuration["PayPal:ClientId"];
            var secret = _configuration["PayPal:Secret"];
            var adminUserName = _configuration["adminLogin:Username"];
            var adminPassword = _configuration["AdminLogin:Password"];
            var connectionString = _configuration["ConnectionStrings:DefaultConnection"];
            var payPalClient = _configuration["PayPal:ClientId"];
            ViewData["PayPalClientId"] = payPalClient;

            return View();
          
        }

        // Home page shows list of items.
        // Item price is set through the ViewBag.

        public IActionResult Shop()
        {
            return View("Shop", "3.55|CAD|4.95|CAD|7.79|CAD");
        }
     

        public IActionResult PayPalConfirmation(PayPalConfirmationModel payPalConfirmationModel)
        {
            TransactionRepo transactionRepo = new TransactionRepo(_context);
            transactionRepo.AddTransaction(payPalConfirmationModel);
            return View(payPalConfirmationModel);
        }



        // This method receives and stores
        // the Paypal transaction details.
        [HttpPost]
        public JsonResult PaySuccess([FromBody] IPN ipn)
        {
            try
            {
                _context.IPNs.Add(ipn);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
            return Json(ipn);
        }
        // Home page shows list of items.
        // Item price is set through the ViewBag.
        public IActionResult Confirmation(string confirmationId)
        {
            IPN transaction =
            _context.IPNs.FirstOrDefault(t => t.paymentID == confirmationId);

            return View("Confirmation", transaction);
        }


        [Authorize]
        public IActionResult SecureArea()
        {
            string userName = User.Identity.Name;
            var registeredUser = _context.MyRegisteredUsers.FirstOrDefault(ru => ru.Email == userName);

            return View(registeredUser);
        }

        public IActionResult Transactions()
        {
            TransactionRepo transactionRepo  = new TransactionRepo(_context);
            IEnumerable<TransactionVM> transactions = transactionRepo.GetAll();
            return View(transactions);
        }


    }
}