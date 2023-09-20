
using dotnetp.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnetp.Service
{
    public interface IUserStoryRepository
    {
        Task<UserStoryModel> GetByIdAsync(string id);
        Task<IEnumerable<UserStoryModel>> GetAllAsync();
        Task AddAsync(UserStoryModel userStory);
        Task UpdateAsync(UserStoryModel userStory);
        Task DeleteAsync(string id);
    }
}
