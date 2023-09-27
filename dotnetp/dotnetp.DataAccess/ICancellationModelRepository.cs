
using dotnetp.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnetp.Service
{
    public interface ICancellationModelRepository
    {
        Task<int> CreateAsync(CancellationModel cancellation);
        Task<CancellationModel> GetByIdAsync(int id);
        Task<List<CancellationModel>> GetAllAsync();
        Task<bool> UpdateAsync(CancellationModel cancellation);
        Task<bool> DeleteAsync(int id);
    }
}
