
using dotnetp.DTO;
using dotnetp.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace dotnetp.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class CancelBookingController : ControllerBase
    {
        private readonly ICancelBookingService _cancelBookingService;

        public CancelBookingController(ICancelBookingService cancelBookingService)
        {
            _cancelBookingService = cancelBookingService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CancelBookingModel>> Get(int id)
        {
            var cancelBooking = await _cancelBookingService.GetByIdAsync(id);

            if (cancelBooking == null)
            {
                return NotFound();
            }

            return cancelBooking;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CancelBookingModel cancelBooking)
        {
            var id = await _cancelBookingService.CreateAsync(cancelBooking);

            return CreatedAtAction(nameof(Get), new { id = id }, cancelBooking);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CancelBookingModel cancelBooking)
        {
            if (id != cancelBooking.Id)
            {
                return BadRequest();
            }

            var result = await _cancelBookingService.UpdateAsync(cancelBooking);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _cancelBookingService.DeleteAsync(id);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
