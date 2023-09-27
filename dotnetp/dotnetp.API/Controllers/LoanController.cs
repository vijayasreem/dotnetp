


using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dotnetp.DTO;
using dotnetp.Service;
using Microsoft.AspNetCore.Mvc;

namespace dotnetp.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoanController : ControllerBase
    {
        private readonly IUserRepositoryService _userRepositoryService;

        public LoanController(IUserRepositoryService userRepositoryService)
        {
            _userRepositoryService = userRepositoryService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> GetUserById(int id)
        {
            var user = await _userRepositoryService.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserModel>>> GetAllUsers()
        {
            var users = await _userRepositoryService.GetAll();
            return users;
        }

        [HttpPost]
        public async Task<ActionResult<UserModel>> CreateUser(UserModel user)
        {
            var createdUser = await _userRepositoryService.Create(user);
            return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserModel user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            var updated = await _userRepositoryService.Update(user);
            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var deleted = await _userRepositoryService.Delete(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost("VerifyDocuments")]
        public async Task<ActionResult<bool>> VerifyDocuments([FromBody] string documentPath)
        {
            try
            {
                bool isValid = await _userRepositoryService.VerifyDocuments(documentPath);
                return Ok(isValid);
            }
            catch (InvalidFileException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("ValidateCreditEvaluation")]
        public async Task<ActionResult<bool>> ValidateCreditEvaluation([FromBody] decimal income)
        {
            bool isValid = await _userRepositoryService.ValidateCreditEvaluation(income);
            return Ok(isValid);
        }

        [HttpPost("CheckCustomerAge")]
        public async Task<ActionResult<bool>> CheckCustomerAge([FromBody] int age)
        {
            bool isValid = await _userRepositoryService.CheckCustomerAge(age);
            return Ok(isValid);
        }

        [HttpPost("CheckCreditScore")]
        public async Task<ActionResult<bool>> CheckCreditScore([FromBody] int creditScore)
        {
            bool isValid = await _userRepositoryService.CheckCreditScore(creditScore);
            return Ok(isValid);
        }

        [HttpPost("ProcessDisbursement")]
        public async Task<ActionResult<bool>> ProcessDisbursement([FromBody] DisbursementRequestModel request)
        {
            try
            {
                bool isSuccessful = await _userRepositoryService.ProcessDisbursement(request.VendorName, request.PaymentAmount, request.BankAccountNumber, request.RoutingNumber, request.AvailableBalance);
                return Ok(isSuccessful);
            }
            catch (InvalidVendorInformationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InsufficientFundsException)
            {
                return BadRequest("Insufficient funds");
            }
        }

        [HttpPost("VerifyVendorInformation")]
        public async Task<ActionResult<bool>> VerifyVendorInformation([FromBody] VendorInformationModel vendorInformation)
        {
            bool isValid = await _userRepositoryService.VerifyVendorInformation(vendorInformation.VendorName, vendorInformation.BankAccountNumber, vendorInformation.RoutingNumber);
            return Ok(isValid);
        }
    }
}
