using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PayPalDemo.ViewModels
{
    public class TransactionVM
    {
        [Key]
        [DisplayName("Payment ID")]
        public string? PaymentId { get; set; }

        [DisplayName("Created")]
        public string? CreatedAt { get; set; }

        [DisplayName("Name")]
        public string? PayerName { get; set; }

        [DisplayName("Email")]
        public string? Email { get; set; }

        [DisplayName("Amount")]
        public string? Amount { get; set; }

        public string? Currency { get; set; }
        [DisplayName("MOP")]
        public string? ModeOfPayment { get; set; }
    }
}
