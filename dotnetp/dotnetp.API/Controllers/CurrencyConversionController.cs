
using dotnetp.DTO;
using dotnetp.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace dotnetp.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class CurrencyConversionController : ControllerBase
    {
        private readonly ICurrencyConversionService _currencyConversionService;

        public CurrencyConversionController(ICurrencyConversionService currencyConversionService)
        {
            _currencyConversionService = currencyConversionService ?? throw new ArgumentNullException(nameof(currencyConversionService));
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateAsync(CurrencyConversionModel model)
        {
            var id = await _currencyConversionService.CreateAsync(model);
            return Ok(id);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CurrencyConversionModel>> GetByIdAsync(int id)
        {
            var model = await _currencyConversionService.GetByIdAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            return Ok(model);
        }

        [HttpGet]
        public async Task<ActionResult<List<CurrencyConversionModel>>> GetAllAsync()
        {
            var models = await _currencyConversionService.GetAllAsync();
            return Ok(models);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, CurrencyConversionModel model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            await _currencyConversionService.UpdateAsync(model);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _currencyConversionService.DeleteAsync(id);
            return NoContent();
        }
    }
}
