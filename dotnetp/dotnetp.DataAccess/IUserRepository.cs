
using System.Collections.Generic;
using System.Threading.Tasks;
using dotnetp.DTO;

namespace dotnetp.Service
{
    public interface IUserRepository
    {
        Task<UserModel> GetById(int id);
        Task<List<UserModel>> GetAll();
        Task<UserModel> Create(UserModel user);
        Task<bool> Update(UserModel user);
        Task<bool> Delete(int id);
    }
}
