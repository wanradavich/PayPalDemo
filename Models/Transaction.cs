namespace PayPalDemo.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public string TransactionId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string PayerName { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
