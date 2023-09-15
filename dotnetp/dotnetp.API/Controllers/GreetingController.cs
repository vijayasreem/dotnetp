
using System.Collections.Generic;
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
        private readonly IGreetingModelService _greetingService;

        public GreetingController(IGreetingModelService greetingService)
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

        [HttpGet]
        public async Task<ActionResult<List<GreetingModel>>> GetAllAsync()
        {
            var greetings = await _greetingService.GetAllAsync();
            return greetings;
        }

        [HttpPost]
        public async Task<ActionResult<int>> AddAsync(GreetingModel greetingModel)
        {
            var id = await _greetingService.AddAsync(greetingModel);
            return id;
        }

        [HttpPut]
        public async Task<ActionResult<bool>> UpdateAsync(GreetingModel greetingModel)
        {
            var result = await _greetingService.UpdateAsync(greetingModel);
            return result;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync(int id)
        {
            var result = await _greetingService.DeleteAsync(id);
            return result;
        }
    }
}
