
using System.Collections.Generic;
using System.Threading.Tasks;
using dotnetp.DTO;
using dotnetp.Service;
using Microsoft.AspNetCore.Mvc;

namespace dotnetp.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepositoryService _userRepositoryService;

        public UserController(IUserRepositoryService userRepositoryService)
        {
            _userRepositoryService = userRepositoryService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> GetByIdAsync(int id)
        {
            var user = await _userRepositoryService.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserModel>>> GetAllAsync()
        {
            var users = await _userRepositoryService.GetAllAsync();
            return users;
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateAsync(UserModel user)
        {
            var createdUserId = await _userRepositoryService.CreateAsync(user);
            return createdUserId;
        }

        [HttpPut]
        public async Task<ActionResult<bool>> UpdateAsync(UserModel user)
        {
            var isUpdated = await _userRepositoryService.UpdateAsync(user);
            return isUpdated;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync(int id)
        {
            var isDeleted = await _userRepositoryService.DeleteAsync(id);
            return isDeleted;
        }
    }
}
