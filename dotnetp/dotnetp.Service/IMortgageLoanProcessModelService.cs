using System.Collections.Generic;
using System.Threading.Tasks;
using dotnetp.DTO;

namespace dotnetp.Service
{
    public interface IMortgageLoanProcessModelService
    {
        Task<int> CreateAsync(MortgageLoanProcessModel model);
        Task<MortgageLoanProcessModel> GetByIdAsync(int id);
        Task<List<MortgageLoanProcessModel>> GetAllAsync();
        Task<int> UpdateAsync(MortgageLoanProcessModel model);
        Task<int> DeleteAsync(int id);
    }
}