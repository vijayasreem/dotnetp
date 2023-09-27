
using System;

namespace dotnetp
{
    public class VerificationModel
    {
        public int Id { get; set; }
        public string VendorName { get; set; }
        public string VendorBankAccountNumber { get; set; }
        public string VendorRoutingNumber { get; set; }
        public bool IsPaymentApprovalRequired { get; set; }
        public decimal PaymentAmount { get; set; }
    }

    public class ValidationModel
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string FileFormat { get; set; }
        public decimal CustomerIncome { get; set; }
        public int CustomerAge { get; set; }
        public int CreditScore { get; set; }
        public decimal AvailableBalance { get; set; }
    }

    public class DisbursementModel
    {
        public int Id { get; set; }
        public string VendorName { get; set; }
        public decimal PaymentAmount { get; set; }
    }
}
