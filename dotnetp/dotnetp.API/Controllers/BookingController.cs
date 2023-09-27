
using dotnetp.DTO;
using dotnetp.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace dotnetp.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly ICancelBookingService _cancelBookingService;

        public BookingController(ICancelBookingService cancelBookingService)
        {
            _cancelBookingService = cancelBookingService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CancelBookingModel>> GetBookingByIdAsync(int id)
        {
            var booking = await _cancelBookingService.GetBookingByIdAsync(id);

            if (booking == null)
            {
                return NotFound();
            }

            return booking;
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateBookingAsync(CancelBookingModel booking)
        {
            var bookingId = await _cancelBookingService.CreateBookingAsync(booking);
            return CreatedAtAction(nameof(GetBookingByIdAsync), new { id = bookingId }, bookingId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBookingAsync(int id, CancelBookingModel booking)
        {
            if (id != booking.Id)
            {
                return BadRequest();
            }

            var isUpdated = await _cancelBookingService.UpdateBookingAsync(booking);

            if (!isUpdated)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookingAsync(int id)
        {
            var isDeleted = await _cancelBookingService.DeleteBookingAsync(id);

            if (!isDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet("{id}/cancel")]
        public async Task<ActionResult<bool>> CanCancelBookingAsync(int id)
        {
            var canCancel = await _cancelBookingService.CanCancelBookingAsync(id);

            if (!canCancel)
            {
                return BadRequest("Cannot cancel booking. Check-in time is less than 24 hours away.");
            }

            return true;
        }
    }
}
