
using dotnetp.DTO;
using dotnetp.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace dotnetp.API
{
    [ApiController]
    [Route("api/currency")]
    public class CurrencyConversionController : ControllerBase
    {
        private readonly ICurrencyConversionModelService _currencyConversionService;

        public CurrencyConversionController(ICurrencyConversionModelService currencyConversionService)
        {
            _currencyConversionService = currencyConversionService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CurrencyConversionModel model)
        {
            try
            {
                int id = await _currencyConversionService.CreateAsync(model);
                return Ok(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                var model = await _currencyConversionService.GetByIdAsync(id);
                if (model == null)
                {
                    return NotFound();
                }
                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var models = await _currencyConversionService.GetAllAsync();
                return Ok(models);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, CurrencyConversionModel model)
        {
            try
            {
                model.Id = id;
                bool result = await _currencyConversionService.UpdateAsync(model);
                if (!result)
                {
                    return NotFound();
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                bool result = await _currencyConversionService.DeleteAsync(id);
                if (!result)
                {
                    return NotFound();
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
