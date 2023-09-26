
using dotnetp.DTO;
using dotnetp.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace dotnetp.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingCancellationController : ControllerBase
    {
        private readonly IBookingCancellationService _bookingCancellationService;

        public BookingCancellationController(IBookingCancellationService bookingCancellationService)
        {
            _bookingCancellationService = bookingCancellationService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookingCancellationModel>> GetById(int id)
        {
            var bookingCancellation = await _bookingCancellationService.GetById(id);

            if (bookingCancellation == null)
            {
                return NotFound();
            }

            return bookingCancellation;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BookingCancellationModel bookingCancellation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _bookingCancellationService.Create(bookingCancellation);

            return CreatedAtAction(nameof(GetById), new { id = bookingCancellation.Id }, bookingCancellation);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] BookingCancellationModel bookingCancellation)
        {
            if (id != bookingCancellation.Id)
            {
                return BadRequest();
            }

            await _bookingCancellationService.Update(bookingCancellation);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var bookingCancellation = await _bookingCancellationService.GetById(id);

            if (bookingCancellation == null)
            {
                return NotFound();
            }

            await _bookingCancellationService.Delete(id);

            return NoContent();
        }

        [HttpGet("CanCancelBooking/{bookingId}/{checkInDate}")]
        public async Task<ActionResult<bool>> CanCancelBooking(int bookingId, DateTime checkInDate)
        {
            var canCancel = await _bookingCancellationService.CanCancelBooking(bookingId, checkInDate);

            return canCancel;
        }
    }
}
