using System.Collections.Generic;
using System.Threading.Tasks;
using dotnetp.DTO;

namespace dotnetp.Service
{
    public interface IUserRepositoryService
    {
        Task<UserModel> GetByIdAsync(int id);
        Task<List<UserModel>> GetAllAsync();
        Task<int> CreateAsync(UserModel user);
        Task<bool> UpdateAsync(UserModel user);
        Task<bool> DeleteAsync(int id);
    }
}