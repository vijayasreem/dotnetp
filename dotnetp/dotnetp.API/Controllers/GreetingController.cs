
using System.Threading.Tasks;
using dotnetp.DTO;
using dotnetp.Service;
using Microsoft.AspNetCore.Mvc;

namespace dotnetp.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class GreetingController : ControllerBase
    {
        private readonly IGreetingService _greetingService;

        public GreetingController(IGreetingService greetingService)
        {
            _greetingService = greetingService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GreetingModel>> GetByIdAsync(int id)
        {
            var greeting = await _greetingService.GetByIdAsync(id);

            if (greeting == null)
            {
                return NotFound();
            }

            return greeting;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(GreetingModel greeting)
        {
            await _greetingService.CreateAsync(greeting);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(GreetingModel greeting)
        {
            await _greetingService.UpdateAsync(greeting);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _greetingService.DeleteAsync(id);

            return Ok();
        }
    }
}
