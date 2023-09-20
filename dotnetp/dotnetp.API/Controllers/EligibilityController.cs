
using dotnetp.DTO;
using dotnetp.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace dotnetp.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class EligibilityController : ControllerBase
    {
        private readonly IUserStoryService _userStoryService;

        public EligibilityController(IUserStoryService userStoryService)
        {
            _userStoryService = userStoryService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserStoryModel>> GetByIdAsync(string id)
        {
            var userStory = await _userStoryService.GetByIdAsync(id);

            if (userStory == null)
            {
                return NotFound();
            }

            return userStory;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserStoryModel>>> GetAllAsync()
        {
            var userStories = await _userStoryService.GetAllAsync();
            return Ok(userStories);
        }

        [HttpPost]
        public async Task<ActionResult> AddAsync(UserStoryModel userStory)
        {
            await _userStoryService.AddAsync(userStory);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = userStory.Id }, userStory);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(string id, UserStoryModel userStory)
        {
            if (id != userStory.Id)
            {
                return BadRequest();
            }

            await _userStoryService.UpdateAsync(userStory);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            var userStory = await _userStoryService.GetByIdAsync(id);

            if (userStory == null)
            {
                return NotFound();
            }

            await _userStoryService.DeleteAsync(id);

            return NoContent();
        }

        [HttpPost("CheckEligibility")]
        public IActionResult CheckEligibility(UserStoryModel userStory)
        {
            bool isEligible = _userStoryService.CheckEligibility(userStory);

            if (isEligible)
            {
                return Ok("Eligible");
            }

            return Ok("Not Eligible");
        }
    }
}
