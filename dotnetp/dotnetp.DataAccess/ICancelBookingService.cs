
using System.Threading.Tasks;
using dotnetp.DTO;

namespace dotnetp.Service
{
    public interface ICancelBookingService
    {
        Task<int> CreateAsync(CancelBookingModel cancelBooking);
        Task<CancelBookingModel> GetByIdAsync(int id);
        Task<bool> UpdateAsync(CancelBookingModel cancelBooking);
        Task<bool> DeleteAsync(int id);
    }
}
