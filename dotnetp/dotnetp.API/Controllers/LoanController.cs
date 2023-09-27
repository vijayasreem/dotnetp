
using dotnetp.DTO;
using dotnetp.Service;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace dotnetp.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanController : ControllerBase
    {
        private readonly IUserStoryService _userStoryService;

        public LoanController(IUserStoryService userStoryService)
        {
            _userStoryService = userStoryService;
        }

        [HttpPost("VerifyDocuments")]
        public async Task<IActionResult> VerifyDocuments(DocumentModel document)
        {
            // Check file format
            if (document.FileFormat != "PDF" && document.FileFormat != "JPEG")
            {
                throw new InvalidFileException("Invalid file format");
            }

            // Verification logic here

            return Ok();
        }

        [HttpPost("ValidateCreditEvaluation")]
        public async Task<IActionResult> ValidateCreditEvaluation(CreditEvaluationModel creditEvaluation)
        {
            // Check customer's income
            if (creditEvaluation.Income <= 100000)
            {
                throw new InvalidCreditEvaluationException("Income should be above 100000");
            }

            // Check customer's age
            if (creditEvaluation.Age < 18 || creditEvaluation.Age > 65)
            {
                throw new InvalidCreditEvaluationException("Age should be between 18 and 65");
            }

            // Check credit score
            if (creditEvaluation.CreditScore <= 600)
            {
                throw new InvalidCreditEvaluationException("Credit score should be above 600");
            }

            // Validation logic here

            return Ok();
        }

        [HttpPost("CheckCustomerAge")]
        public async Task<IActionResult> CheckCustomerAge(CustomerAgeModel customerAge)
        {
            // Check customer's age
            if (customerAge.Age < 18 || customerAge.Age > 65)
            {
                throw new InvalidCustomerAgeException("Age should be between 18 and 65");
            }

            // Check customer age logic here

            return Ok();
        }

        [HttpPost("CheckCreditScore")]
        public async Task<IActionResult> CheckCreditScore(CreditScoreModel creditScore)
        {
            // Check credit score
            if (creditScore.Score <= 600)
            {
                throw new InvalidCreditScoreException("Credit score should be above 600");
            }

            // Check credit score logic here

            return Ok();
        }

        [HttpPost("ProcessDisbursement")]
        public async Task<IActionResult> ProcessDisbursement(DisbursementModel disbursement)
        {
            // Check vendor's bank account number and routing number
            if (disbursement.BankAccountNumber.Length != 9 || disbursement.RoutingNumber.Length != 9)
            {
                throw new InvalidVendorInformationException("Invalid vendor information");
            }

            // Check available balance
            if (disbursement.AvailableBalance < disbursement.PaymentAmount)
            {
                throw new InsufficientFundsException("Insufficient funds");
            }

            // Check payment amount for automatic approval
            if (disbursement.PaymentAmount <= 1000.0)
            {
                return Ok("Payment automatically approved");
            }

            // Prompt for payment approval logic here

            return Ok("Disbursement process successful");
        }
    }
}
