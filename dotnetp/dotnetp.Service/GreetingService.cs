using System.Threading.Tasks;
using dotnetp.DataAccess;
using dotnetp.DTO;

namespace dotnetp.Service
{
    public class GreetingService : IGreetingService
    {
        private readonly GreetingDataAccess _greetingDataAccess;

        public GreetingService()
        {
            _greetingDataAccess = new GreetingDataAccess();
        }

        public async Task<GreetingModel> GetByIdAsync(int id)
        {
            return await _greetingDataAccess.GetByIdAsync(id);
        }

        public async Task CreateAsync(GreetingModel greeting)
        {
            await _greetingDataAccess.CreateAsync(greeting);
        }

        public async Task UpdateAsync(GreetingModel greeting)
        {
            await _greetingDataAccess.UpdateAsync(greeting);
        }

        public async Task DeleteAsync(int id)
        {
            await _greetingDataAccess.DeleteAsync(id);
        }
    }
}