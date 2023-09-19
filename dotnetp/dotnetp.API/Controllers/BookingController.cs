
using System.Threading.Tasks;
using dotnetp.DTO;
using dotnetp.Service;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult<int>> CreateAsync(BookingModel booking)
        {
            var bookingId = await _bookingService.CreateAsync(booking);
            return Ok(bookingId);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookingModel>> GetByIdAsync(int id)
        {
            var booking = await _bookingService.GetByIdAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            return Ok(booking);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, BookingModel booking)
        {
            if (id != booking.Id)
            {
                return BadRequest();
            }

            await _bookingService.UpdateAsync(booking);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _bookingService.DeleteAsync(id);
            return NoContent();
        }
    }
}
