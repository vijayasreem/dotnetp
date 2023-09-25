
using dotnetp.DTO;
using dotnetp.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace dotnetp.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class InsuranceController : ControllerBase
    {
        private readonly IUserStoryService _userStoryService;

        public InsuranceController(IUserStoryService userStoryService)
        {
            _userStoryService = userStoryService;
        }

        [HttpPost]
        public async Task<IActionResult> CheckEligibility([FromBody] UserStoryModel userStoryModel)
        {
            try
            {
                bool isEligible = await _userStoryService.CheckEligibilityAsync(userStoryModel);

                if (isEligible)
                {
                    return Ok("Eligible");
                }
                else
                {
                    return Ok("Not Eligible");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
