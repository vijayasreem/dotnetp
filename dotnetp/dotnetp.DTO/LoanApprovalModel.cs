namespace Dotnetp
{
    public class LoanApprovalModel
    {
        public int Id { get; set; }
        public string Identification { get; set; }
        public string ProofOfIncome { get; set; }
        public string CreditHistory { get; set; }
        public string EmploymentDetails { get; set; }
        public string CreditCheck { get; set; }
        public int LoanAmount { get; set; }
        public float InterestRate { get; set; }
        public string VehicleValueAssessment { get; set; }
        public string LoanOfferAcceptance { get; set; }
        public int DisbursedLoanAmount { get; set; }
    }
}