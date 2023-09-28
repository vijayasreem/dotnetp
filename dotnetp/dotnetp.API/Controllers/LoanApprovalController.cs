using dotnetp.API.DTO;
using dotnetp.API.Service;
using Microsoft.AspNetCore.Mvc;
 
namespace dotnetp.API
{
    [ApiController]
    public class LoanApprovalController : ControllerBase
    {
        private readonly ILoanApprovalRepository _loanApprovalRepository;
 
        public LoanApprovalController(ILoanApprovalRepository loanApprovalRepository)
        {
            _loanApprovalRepository = loanApprovalRepository;
        }

        [HttpGet]
        [Route("api/loanApproval/{id}")]
        public async Task<ActionResult<LoanApprovalModel>> GetLoanApprovalByIdAsync(int id)
        {
            var loanApproval = await _loanApprovalRepository.GetLoanApprovalByIdAsync(id);
            if (loanApproval == null)
            {
                return NotFound();
            }
            return loanApproval;
        }

        [HttpPost]
        [Route("api/loanApproval")]
        public async Task<ActionResult<LoanApprovalModel>> CreateLoanApprovalAsync([FromBody]LoanApprovalModel loanApproval)
        {
            var id = await _loanApprovalRepository.CreateLoanApprovalAsync(loanApproval);
            loanApproval.Id = id;
            return CreatedAtAction(nameof(GetLoanApprovalByIdAsync), new { id = id }, loanApproval);
        }

        [HttpPut]
        [Route("api/loanApproval/{id}")]
        public async Task<ActionResult<LoanApprovalModel>> UpdateLoanApprovalAsync(int id, [FromBody]LoanApprovalModel loanApproval)
        {
            // Perform credit check and pre-qualification.
            // Assess the value of the vehicle.
            // Check if the applicant has accepted the loan offer.
            // Disburse the approved loan amount.
            var result = await _loanApprovalRepository.UpdateLoanApprovalAsync(loanApproval);
            if (result == 0)
            {
                return NotFound();
            }
            return loanApproval;
        }
    }
}