
namespace dotnetp
{
    public class MortgageLoanProcessModel
    {
        public int Id { get; set; }

        public string CustomerInformation { get; set; }

        public string VerifiedDocuments { get; set; }

        public bool CreditCheckPassed { get; set; }

        public bool PreQualified { get; set; }

        public bool LoanApproved { get; set; }

        public string VehicleAssessment { get; set; }

        public bool LoanAccepted { get; set; }

        public decimal LoanAmount { get; set; }

        public bool LoanDisbursed { get; set; }

        public string Results { get; set; }
    }
}
