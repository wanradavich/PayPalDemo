using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PayPalDemo.Models;

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

            var adminUserName = _configuration["adminLogin:Username"];
            var adminPassword = _configuration["AdminLogin:Password"];
            var connectionString = _configuration["ConnectionStrings:DefaultConnection"];

            return View();
            //return View("Index", "3.55|CAD");
        }

        // Home page shows list of items.
        // Item price is set through the ViewBag.

        public IActionResult Shop()
        {
            return View("Shop", "3.55|CAD|4.95|CAD|7.79|CAD");
        }
        public IActionResult Transactions()
        {

            //DbSet<IPN> items = _context.IPNs;
            DbSet<Transaction> items = _context.transactions;


            return View(items);
        }

        public IActionResult PayPalConfirmation(PayPalConfirmationModel payPalConfirmationModel)
        {
            return View(payPalConfirmationModel);
        }

        [HttpPost]
        public IActionResult PayPalConfirmation(string transactionId, decimal amount, string currency, string payerName)
        {
            // Save the transaction to the database
            var transaction = new Transaction
            {
                TransactionId = transactionId,
                Amount = amount,
                Currency = currency,
                PayerName = payerName,
                Timestamp = DateTime.Now
            };

            _context.transactions.Add(transaction);
            _context.SaveChanges();

            return RedirectToAction("Transactions");
        }

        // This method receives and stores
        // the Paypal transaction details.
        //[HttpPost]
        //public JsonResult PaySuccess([FromBody] IPN ipn)
        //{
        //    try
        //    {
        //        _context.IPNs.Add(ipn);
        //        _context.SaveChanges();
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(ex.Message);
        //    }
        //    return Json(ipn);
        //}
        // Home page shows list of items.
        // Item price is set through the ViewBag.
        public IActionResult Confirmation(string confirmationId)
        {
            Transaction transaction =
            _context.transactions.FirstOrDefault(t => t.TransactionId == confirmationId);

            return View("Confirmation", transaction);
        }

       

        //[Authorize]
        public IActionResult SecureArea()
        {
            string userName = User.Identity.Name;
            var registeredUser = _context.MyRegisteredUsers.FirstOrDefault(ru => ru.Email == userName);

            return View(registeredUser);
        }



    }
}