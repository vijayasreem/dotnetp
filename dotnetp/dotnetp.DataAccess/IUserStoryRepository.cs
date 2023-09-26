


using System.Collections.Generic;
using System.Threading.Tasks;
using dotnetp.DTO;

namespace dotnetp.Service
{
    public interface IUserStoryRepository
    {
        Task<UserStoryModel> GetByIdAsync(int id);
        Task<List<UserStoryModel>> GetAllAsync();
        Task<int> CreateAsync(UserStoryModel userStory);
        Task UpdateAsync(UserStoryModel userStory);
        Task DeleteAsync(int id);
    }
}
