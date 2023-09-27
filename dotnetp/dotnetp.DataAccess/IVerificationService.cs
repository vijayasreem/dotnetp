


using System.Collections.Generic;
using System.Threading.Tasks;
using dotnetp.DTO;

namespace dotnetp.Service
{
    public interface IVerificationService
    {
        Task<int> AddAsync(VerificationModel verification);
        Task<VerificationModel> GetByIdAsync(int id);
        Task<List<VerificationModel>> GetAllAsync();
        Task<bool> UpdateAsync(VerificationModel verification);
        Task<bool> DeleteAsync(int id);
    }

    public interface IValidationService
    {
        Task<int> AddAsync(ValidationModel validation);
        Task<ValidationModel> GetByIdAsync(int id);
        Task<List<ValidationModel>> GetAllAsync();
        Task<bool> UpdateAsync(ValidationModel validation);
        Task<bool> DeleteAsync(int id);
    }

    public interface IDisbursementService
    {
        Task<int> AddAsync(DisbursementModel disbursement);
        Task<DisbursementModel> GetByIdAsync(int id);
        Task<List<DisbursementModel>> GetAllAsync();
        Task<bool> UpdateAsync(DisbursementModel disbursement);
        Task<bool> DeleteAsync(int id);
    }
}
