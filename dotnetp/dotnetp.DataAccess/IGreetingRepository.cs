


using System.Threading.Tasks;
using dotnetp.DTO;

namespace dotnetp.Service
{
    public interface IGreetingRepository
    {
        Task<GreetingModel> GetByIdAsync(int id);
        Task CreateAsync(GreetingModel greeting);
        Task UpdateAsync(GreetingModel greeting);
        Task DeleteAsync(int id);
    }
}
