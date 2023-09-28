
namespace dotnetp
{
    public class LoanApprovalModel
    {
        public int Id { get; set; }

        public string ValidIdentification { get; set; }

        public string ProofOfIncome { get; set; }

        public string CreditHistory { get; set; }

        public string EmploymentDetails { get; set; }

        public bool CreditCheckPerformed { get; set; }

        public decimal LoanAmount { get; set; }

        public decimal InterestRateRange { get; set; }

        public bool VehicleAssessmentRequired { get; set; }

        public decimal VehicleValue { get; set; }

        public bool LoanOfferAccepted { get; set; }

        public bool LoanDisbursed { get; set; }
    }
}
