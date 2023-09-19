
using dotnetp.DTO;
using dotnetp.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnetp.API
{
    [ApiController]
    [Route("api/creditcards")]
    public class CreditCardController : ControllerBase
    {
        private readonly ICreditCardRepository _creditCardRepository;

        public CreditCardController(ICreditCardRepository creditCardRepository)
        {
            _creditCardRepository = creditCardRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreditCardModel creditCard)
        {
            try
            {
                int id = await _creditCardRepository.CreateAsync(creditCard);
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
                CreditCardModel creditCard = await _creditCardRepository.GetByIdAsync(id);
                if (creditCard == null)
                {
                    return NotFound();
                }
                return Ok(creditCard);
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
                List<CreditCardModel> creditCards = await _creditCardRepository.GetAllAsync();
                return Ok(creditCards);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, CreditCardModel creditCard)
        {
            try
            {
                creditCard.Id = id;
                await _creditCardRepository.UpdateAsync(creditCard);
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
                await _creditCardRepository.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
