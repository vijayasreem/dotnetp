


using System.Collections.Generic;
using System.Threading.Tasks;
using dotnetp.DTO;

namespace dotnetp.Service
{
    public interface IEndorsementRepository
    {
        Task<int> CreateAsync(EndorsementModel endorsement);
        Task<EndorsementModel> GetByIdAsync(int id);
        Task<IEnumerable<EndorsementModel>> GetAllAsync();
        Task UpdateAsync(EndorsementModel endorsement);
        Task DeleteAsync(int id);
    }
}
