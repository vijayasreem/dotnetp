
using dotnetp.DTO;
using dotnetp.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace dotnetp.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingModelService _bookingService;

        public BookingController(IBookingModelService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking(BookingModel booking)
        {
            await _bookingService.CreateAsync(booking);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookingById(int id)
        {
            var booking = await _bookingService.GetByIdAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            return Ok(booking);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBooking(int id, BookingModel booking)
        {
            if (id != booking.Id)
            {
                return BadRequest();
            }
            await _bookingService.UpdateAsync(booking);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            await _bookingService.DeleteAsync(id);
            return Ok();
        }

        [HttpPost("{id}/cancel")]
        public async Task<IActionResult> CancelBooking(int id)
        {
            var booking = await _bookingService.GetByIdAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            var canCancel = await _bookingService.CanCancelBookingAsync(booking);
            if (!canCancel)
            {
                return BadRequest("Booking cannot be cancelled.");
            }

            await _bookingService.CancelBookingAsync(booking);
            return Ok();
        }
    }
}
