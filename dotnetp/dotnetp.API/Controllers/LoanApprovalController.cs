
using dotnetp.DTO;
using dotnetp.Service;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnetp.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanApprovalController : ControllerBase
    {
        private readonly ILoanApprovalModelService _loanApprovalService;

        public LoanApprovalController(ILoanApprovalModelService loanApprovalService)
        {
            _loanApprovalService = loanApprovalService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(LoanApprovalModel loanApprovalModel)
        {
            await _loanApprovalService.CreateAsync(loanApprovalModel);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LoanApprovalModel>> GetByIdAsync(int id)
        {
            var loanApprovalModel = await _loanApprovalService.GetByIdAsync(id);
            if (loanApprovalModel == null)
            {
                return NotFound();
            }
            return loanApprovalModel;
        }

        [HttpGet]
        public async Task<ActionResult<List<LoanApprovalModel>>> GetAllAsync()
        {
            var loanApprovalModels = await _loanApprovalService.GetAllAsync();
            return loanApprovalModels;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, LoanApprovalModel loanApprovalModel)
        {
            if (id != loanApprovalModel.Id)
            {
                return BadRequest();
            }
            await _loanApprovalService.UpdateAsync(loanApprovalModel);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _loanApprovalService.DeleteAsync(id);
            return Ok();
        }
    }
}
