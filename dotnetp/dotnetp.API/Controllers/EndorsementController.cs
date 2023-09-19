
using dotnetp.DTO;
using dotnetp.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnetp.API
{
    [ApiController]
    [Route("api/endorsements")]
    public class EndorsementController : ControllerBase
    {
        private readonly IEndorsementRepository _endorsementRepository;
        private readonly ILogger _logger;

        public EndorsementController(IEndorsementRepository endorsementRepository, ILogger logger)
        {
            _endorsementRepository = endorsementRepository;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(EndorsementModel endorsement)
        {
            try
            {
                int endorsementId = await _endorsementRepository.CreateAsync(endorsement);
                await LogActionAsync("Create", endorsementId);
                return Ok(endorsementId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create endorsement");
                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                var endorsement = await _endorsementRepository.GetByIdAsync(id);
                if (endorsement == null)
                {
                    return NotFound();
                }
                await LogActionAsync("GetById", id);
                return Ok(endorsement);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get endorsement by ID");
                return StatusCode(500);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var endorsements = await _endorsementRepository.GetAllAsync();
                await LogActionAsync("GetAll");
                return Ok(endorsements);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get all endorsements");
                return StatusCode(500);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, EndorsementModel endorsement)
        {
            try
            {
                await _endorsementRepository.UpdateAsync(endorsement);
                await LogActionAsync("Update", id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update endorsement");
                return StatusCode(500);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                await _endorsementRepository.DeleteAsync(id);
                await LogActionAsync("Delete", id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete endorsement");
                return StatusCode(500);
            }
        }

        private async Task LogActionAsync(string action, int endorsementId = 0)
        {
            // Implement logging of user actions, including maker and checker activities
            // Log timestamps, user IDs, and relevant details for each action
            // Store the history logs in a designated database table for future reference
            // You can use your preferred logging mechanism and database storage here

            // Example log format:
            string logMessage = $"Action: {action}, Endorsement ID: {endorsementId}, User ID: {User.Identity.Name}, Timestamp: {DateTime.Now}";

            // Example log to console
            Console.WriteLine(logMessage);

            // Example log to database using a logging service
            await _logger.LogAsync(logMessage);
        }
    }
}
