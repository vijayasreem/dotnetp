
using dotnetp.DTO;
using dotnetp.Service;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnetp.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoanApprovalController : ControllerBase
    {
        private readonly ILoanApprovalModelRepository _loanApprovalModelRepository;

        public LoanApprovalController(ILoanApprovalModelRepository loanApprovalModelRepository)
        {
            _loanApprovalModelRepository = loanApprovalModelRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(LoanApprovalModel loanApprovalModel)
        {
            await _loanApprovalModelRepository.CreateAsync(loanApprovalModel);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var loanApprovalModel = await _loanApprovalModelRepository.GetByIdAsync(id);
            return Ok(loanApprovalModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var loanApprovalModels = await _loanApprovalModelRepository.GetAllAsync();
            return Ok(loanApprovalModels);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(LoanApprovalModel loanApprovalModel)
        {
            await _loanApprovalModelRepository.UpdateAsync(loanApprovalModel);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _loanApprovalModelRepository.DeleteAsync(id);
            return Ok();
        }
    }
}
