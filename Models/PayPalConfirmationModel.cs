﻿namespace PayPalDemo.Models
{
    public class PayPalConfirmationModel
    {
        public string TransactionId { get; set; }
        public string Amount { get; set; }
        public string PayerName { get; set; }

        public string PublicEmail { get; set; }
        public string  PaymentMethod { get; set; }
    }

}
