
using dotnetp.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnetp.Service
{
    public interface IUserStoryService
    {
        Task<int> CreateAsync(UserStoryModel model);
        Task<UserStoryModel> GetByIdAsync(int id);
        Task<List<UserStoryModel>> GetAllAsync();
        Task UpdateAsync(UserStoryModel model);
        Task DeleteAsync(int id);
    }
}
