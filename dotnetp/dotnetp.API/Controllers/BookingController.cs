using System;
using System.Threading.Tasks;
using dotnetp.DTO;
using dotnetp.Service;
using Microsoft.AspNetCore.Mvc;

namespace dotnetp.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingModelService _bookingService;

        public BookingController(IBookingModelService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBookingAsync(BookingModel booking)
        {
            try
            {
                int bookingId = await _bookingService.CreateBookingAsync(booking);
                return Ok(bookingId);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookingByIdAsync(int id)
        {
            try
            {
                BookingModel booking = await _bookingService.GetBookingByIdAsync(id);
                if (booking == null)
                {
                    return NotFound();
                }
                return Ok(booking);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBookingAsync(BookingModel booking)
        {
            try
            {
                bool result = await _bookingService.UpdateBookingAsync(booking);
                if (!result)
                {
                    return NotFound();
                }
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookingAsync(int id)
        {
            try
            {
                bool result = await _bookingService.DeleteBookingAsync(id);
                if (!result)
                {
                    return NotFound();
                }
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}