using Microsoft.EntityFrameworkCore;
using PayPalDemo.Models;
using PayPalDemo.ViewModels;

namespace PayPalDemo.Repositories
{
    public class TransactionRepo
    {
        private readonly ApplicationDbContext _context;

        public TransactionRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<TransactionVM> GetAll()
        {
            return _context.Transactions;
        }
        public string AddTransaction(PayPalConfirmationModel payPalConfirmationModel)
        {
            var transaction = new TransactionVM()
            {
                PaymentId = payPalConfirmationModel.TransactionId,
                CreatedAt = DateTime.Now.ToString("dd-MM-yyyy, HH:mm"),
                PayerName = payPalConfirmationModel.PayerName,
                Email = payPalConfirmationModel.PublicEmail,
                Amount = payPalConfirmationModel.Amount,
                Currency = "CAD",
                ModeOfPayment = "paypal"
            };
            _context.Transactions.Add(transaction);
            _context.SaveChanges();
            return "Transaction added successfully";
        }
    }
}
