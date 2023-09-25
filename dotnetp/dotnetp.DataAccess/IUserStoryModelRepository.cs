


using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dotnetp.DTO;

namespace dotnetp.Service
{
    public interface IUserStoryModelRepository
    {
        Task<UserStoryModel> GetByIdAsync(int id);
        Task<List<UserStoryModel>> GetAllAsync();
        Task<int> AddAsync(UserStoryModel userStoryModel);
        Task<bool> UpdateAsync(UserStoryModel userStoryModel);
        Task<bool> DeleteAsync(int id);
    }
}
