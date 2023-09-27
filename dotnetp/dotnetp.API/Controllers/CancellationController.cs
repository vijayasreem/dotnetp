
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dotnetp.DTO;
using dotnetp.Service;
using Microsoft.AspNetCore.Mvc;

namespace dotnetp.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class CancellationController : ControllerBase
    {
        private readonly ICancellationModelService _cancellationService;

        public CancellationController(ICancellationModelService cancellationService)
        {
            _cancellationService = cancellationService;
        }

        [HttpGet]
        public async Task<ActionResult<List<CancellationModel>>> GetAllAsync()
        {
            var cancellations = await _cancellationService.GetAllAsync();
            return Ok(cancellations);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CancellationModel>> GetByIdAsync(int id)
        {
            var cancellation = await _cancellationService.GetByIdAsync(id);
            if (cancellation == null)
            {
                return NotFound();
            }
            return Ok(cancellation);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateAsync(CancellationModel cancellation)
        {
            var id = await _cancellationService.CreateAsync(cancellation);
            return Ok(id);
        }

        [HttpPut]
        public async Task<ActionResult<bool>> UpdateAsync(CancellationModel cancellation)
        {
            var result = await _cancellationService.UpdateAsync(cancellation);
            if (!result)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync(int id)
        {
            var result = await _cancellationService.DeleteAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost("cancel-booking/{bookingId}")]
        public async Task<ActionResult<bool>> CancelBookingAsync(int bookingId)
        {
            var result = await _cancellationService.CancelBookingAsync(bookingId);
            if (!result)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
