using System.Collections.Generic;
using System.Threading.Tasks;
using dotnetp.DataAccess;
using dotnetp.DTO;

namespace dotnetp.Service
{
    public class MortgageLoanProcessModelService : IMortgageLoanProcessModelService
    {
        private readonly IMortgageLoanProcessModelDataAccess _dataAccess;

        public MortgageLoanProcessModelService(IMortgageLoanProcessModelDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<int> CreateAsync(MortgageLoanProcessModel model)
        {
            // Implement create logic here
            // Example: return await _dataAccess.CreateAsync(model);
        }

        public async Task<MortgageLoanProcessModel> GetByIdAsync(int id)
        {
            // Implement get by id logic here
            // Example: return await _dataAccess.GetByIdAsync(id);
        }

        public async Task<List<MortgageLoanProcessModel>> GetAllAsync()
        {
            // Implement get all logic here
            // Example: return await _dataAccess.GetAllAsync();
        }

        public async Task<int> UpdateAsync(MortgageLoanProcessModel model)
        {
            // Implement update logic here
            // Example: return await _dataAccess.UpdateAsync(model);
        }

        public async Task<int> DeleteAsync(int id)
        {
            // Implement delete logic here
            // Example: return await _dataAccess.DeleteAsync(id);
        }
    }

    public interface IMortgageLoanProcessModelService : IMortgageLoanProcessModelDataAccess
    {
        Task<int> CreateAsync(MortgageLoanProcessModel model);
        Task<MortgageLoanProcessModel> GetByIdAsync(int id);
        Task<List<MortgageLoanProcessModel>> GetAllAsync();
        Task<int> UpdateAsync(MortgageLoanProcessModel model);
        Task<int> DeleteAsync(int id);
    }
}