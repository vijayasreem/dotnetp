using System.Collections.Generic;
using System.Threading.Tasks;
using dotnetp.DataAccess;
using dotnetp.DTO;

namespace dotnetp.Service
{
    public class UserRepositoryService : IUserRepositoryService
    {
        private readonly IUserRepositoryDataAccess _userRepositoryDataAccess;

        public UserRepositoryService(IUserRepositoryDataAccess userRepositoryDataAccess)
        {
            _userRepositoryDataAccess = userRepositoryDataAccess;
        }

        public async Task<UserModel> GetByIdAsync(int id)
        {
            return await _userRepositoryDataAccess.GetByIdAsync(id);
        }

        public async Task<List<UserModel>> GetAllAsync()
        {
            return await _userRepositoryDataAccess.GetAllAsync();
        }

        public async Task<int> CreateAsync(UserModel user)
        {
            return await _userRepositoryDataAccess.CreateAsync(user);
        }

        public async Task<bool> UpdateAsync(UserModel user)
        {
            return await _userRepositoryDataAccess.UpdateAsync(user);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _userRepositoryDataAccess.DeleteAsync(id);
        }
    }
}