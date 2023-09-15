using System.Collections.Generic;
using System.Threading.Tasks;
using dotnetp.DTO;

namespace dotnetp.Service
{
    public interface IGreetingModelService
    {
        Task<GreetingModel> GetByIdAsync(int id);
        Task<List<GreetingModel>> GetAllAsync();
        Task<int> AddAsync(GreetingModel greetingModel);
        Task<bool> UpdateAsync(GreetingModel greetingModel);
        Task<bool> DeleteAsync(int id);
    }
}