using System.Collections.Generic;
using System.Threading.Tasks;
using dotnetp.DTO;
using dotnetp.Service;
using Microsoft.AspNetCore.Mvc;

namespace dotnetp.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class MortgageLoanProcessModelController : ControllerBase
    {
        private readonly IMortgageLoanProcessModelService _service;

        public MortgageLoanProcessModelController(IMortgageLoanProcessModelService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(MortgageLoanProcessModel model)
        {
            var id = await _service.CreateAsync(model);
            return Ok(id);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(MortgageLoanProcessModel model)
        {
            var updatedRows = await _service.UpdateAsync(model);
            if (updatedRows == 0)
            {
                return NotFound();
            }
            return Ok(updatedRows);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var deletedRows = await _service.DeleteAsync(id);
            if (deletedRows == 0)
            {
                return NotFound();
            }
            return Ok(deletedRows);
        }
    }
}