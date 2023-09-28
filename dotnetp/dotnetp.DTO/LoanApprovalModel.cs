
using System;

namespace dotnetp
{
    public class LoanApprovalModel
    {
        public int Id { get; set; }
        public string ValidIdentification { get; set; }
        public string ProofOfIncome { get; set; }
        public string CreditHistory { get; set; }
        public string EmploymentDetails { get; set; }
        public decimal LoanAmount { get; set; }
        public decimal InterestRate { get; set; }
        public string VehicleValue { get; set; }
        public bool LoanOfferAccepted { get; set; }
        public DateTime DisbursementDate { get; set; }
    }
}
