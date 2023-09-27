
using dotnetp.DTO;
using dotnetp.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace dotnetp.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoanController : ControllerBase
    {
        private readonly IVerificationService _verificationService;
        private readonly IValidationService _validationService;
        private readonly IDisbursementService _disbursementService;

        public LoanController(
            IVerificationService verificationService,
            IValidationService validationService,
            IDisbursementService disbursementService)
        {
            _verificationService = verificationService;
            _validationService = validationService;
            _disbursementService = disbursementService;
        }

        [HttpPost("verifyDocuments")]
        public async Task<IActionResult> VerifyDocuments([FromBody] DocumentModel document)
        {
            try
            {
                if (document.FileType != FileType.PDF && document.FileType != FileType.JPEG)
                {
                    throw new InvalidFileException("Invalid file format. Only PDF and JPEG are supported.");
                }

                // Call verification service to verify documents
                // ...

                return Ok("Documents verified successfully.");
            }
            catch (InvalidFileException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while verifying documents.");
            }
        }

        [HttpPost("validateCreditEvaluation")]
        public async Task<IActionResult> ValidateCreditEvaluation([FromBody] CreditEvaluationModel creditEvaluation)
        {
            try
            {
                if (creditEvaluation.Income <= 100000)
                {
                    return BadRequest("Income should be above 100000 for salaried employees.");
                }

                if (creditEvaluation.Age < 18 || creditEvaluation.Age > 65)
                {
                    return BadRequest("Customer's age should be between 18 and 65.");
                }

                if (creditEvaluation.CreditScore <= 600)
                {
                    return BadRequest("Credit score should be above 600.");
                }

                // Call validation service to validate credit evaluation
                // ...

                return Ok("Credit evaluation validated successfully.");
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while validating credit evaluation.");
            }
        }

        [HttpPost("checkCustomerAge")]
        public async Task<IActionResult> CheckCustomerAge([FromBody] CustomerAgeModel customerAge)
        {
            try
            {
                if (customerAge.Age < 18 || customerAge.Age > 65)
                {
                    return BadRequest("Customer's age should be between 18 and 65.");
                }

                // Call validation service to check customer age
                // ...

                return Ok("Customer age checked successfully.");
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while checking customer age.");
            }
        }

        [HttpPost("checkCreditScore")]
        public async Task<IActionResult> CheckCreditScore([FromBody] CreditScoreModel creditScore)
        {
            try
            {
                if (creditScore.CreditScore <= 600)
                {
                    return BadRequest("Credit score should be above 600.");
                }

                // Call validation service to check credit score
                // ...

                return Ok("Credit score checked successfully.");
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while checking credit score.");
            }
        }

        [HttpPost("processDisbursement")]
        public async Task<IActionResult> ProcessDisbursement([FromBody] DisbursementModel disbursement)
        {
            try
            {
                if (disbursement.VendorBankAccountNumber.Length != 9 || disbursement.VendorRoutingNumber.Length != 9)
                {
                    return BadRequest("Vendor's bank account number and routing number should both be nine characters in length.");
                }

                if (disbursement.AvailableBalance < disbursement.PaymentAmount)
                {
                    return BadRequest("Insufficient funds for disbursement.");
                }

                if (disbursement.PaymentAmount <= 1000.0)
                {
                    // Automatically approve payment
                    // ...

                    return Ok($"Payment of {disbursement.PaymentAmount} automatically approved.");
                }
                else
                {
                    // Prompt for payment approval
                    // ...

                    return Ok($"Payment of {disbursement.PaymentAmount} requires approval.");
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing disbursement.");
            }
        }
    }
}
