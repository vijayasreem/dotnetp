namespace dotnetp
{
    public class UserStoryModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string AcceptanceCriteria { get; set; }
    }

    public class DocumentVerificationModel
    {
        public int Id { get; set; }
        public string FileFormat { get; set; }
    }

    public class CreditEvaluationModel
    {
        public int Id { get; set; }
        public double CustomerIncome { get; set; }
        public int CustomerAge { get; set; }
        public int CreditScore { get; set; }
    }

    public class CustomerAgeVerificationModel
    {
        public int Id { get; set; }
        public int CustomerAge { get; set; }
    }

    public class CreditScoreVerificationModel
    {
        public int Id { get; set; }
        public int CreditScore { get; set; }
    }

    public class VendorBankAccountModel
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public string RoutingNumber { get; set; }
    }

    public class PaymentModel
    {
        public int Id { get; set; }
        public double AvailableBalance { get; set; }
        public double PaymentAmount { get; set; }
    }

    public class DisbursementModel
    {
        public int Id { get; set; }
        public string VendorName { get; set; }
        public double PaymentAmount { get; set; }
    }

    public class VendorInformationModel
    {
        public int Id { get; set; }
        public bool IsVerified { get; set; }
    }

    public class PaymentApprovalModel
    {
        public int Id { get; set; }
        public bool IsApproved { get; set; }
    }
}