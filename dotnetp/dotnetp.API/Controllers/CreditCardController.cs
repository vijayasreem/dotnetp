
using System.Collections.Generic;
using System.Threading.Tasks;
using dotnetp.DTO;
using dotnetp.Service;
using Microsoft.AspNetCore.Mvc;

namespace dotnetp.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class CreditCardController : ControllerBase
    {
        private readonly ICreditCardService _creditCardService;

        public CreditCardController(ICreditCardService creditCardService)
        {
            _creditCardService = creditCardService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CreditCardModel>>> GetAllAsync()
        {
            var creditCards = await _creditCardService.GetAllAsync();
            return Ok(creditCards);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CreditCardModel>> GetByIdAsync(int id)
        {
            var creditCard = await _creditCardService.GetByIdAsync(id);
            if (creditCard == null)
            {
                return NotFound();
            }
            return creditCard;
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateAsync(CreditCardModel creditCard)
        {
            var createdId = await _creditCardService.CreateAsync(creditCard);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = createdId }, createdId);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> UpdateAsync(int id, CreditCardModel creditCard)
        {
            if (id != creditCard.Id)
            {
                return BadRequest();
            }
            var updated = await _creditCardService.UpdateAsync(creditCard);
            if (!updated)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync(int id)
        {
            var deleted = await _creditCardService.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
