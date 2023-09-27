
using dotnetp.DTO;
using dotnetp.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace dotnetp.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingCancellationController : ControllerBase
    {
        private readonly IBookingCancellationService _bookingCancellationService;

        public BookingCancellationController(IBookingCancellationService bookingCancellationService)
        {
            _bookingCancellationService = bookingCancellationService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(BookingCancellationModel bookingCancellation)
        {
            try
            {
                var id = await _bookingCancellationService.CreateAsync(bookingCancellation);
                return Ok(id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                var bookingCancellation = await _bookingCancellationService.GetByIdAsync(id);
                if (bookingCancellation == null)
                {
                    return NotFound();
                }
                return Ok(bookingCancellation);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, BookingCancellationModel bookingCancellation)
        {
            try
            {
                if (id != bookingCancellation.Id)
                {
                    return BadRequest();
                }
                await _bookingCancellationService.UpdateAsync(bookingCancellation);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                await _bookingCancellationService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
