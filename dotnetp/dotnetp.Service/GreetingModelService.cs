using System.Collections.Generic;
using System.Threading.Tasks;
using dotnetp.DataAccess;
using dotnetp.DTO;

namespace dotnetp.Service
{
    public class GreetingModelService : IGreetingModelService
    {
        private readonly IDataAccess _dataAccess;

        public GreetingModelService(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<GreetingModel> GetByIdAsync(int id)
        {
            return await _dataAccess.GetByIdAsync(id);
        }

        public async Task<List<GreetingModel>> GetAllAsync()
        {
            return await _dataAccess.GetAllAsync();
        }

        public async Task<int> AddAsync(GreetingModel greetingModel)
        {
            return await _dataAccess.AddAsync(greetingModel);
        }

        public async Task<bool> UpdateAsync(GreetingModel greetingModel)
        {
            return await _dataAccess.UpdateAsync(greetingModel);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _dataAccess.DeleteAsync(id);
        }
    }
}