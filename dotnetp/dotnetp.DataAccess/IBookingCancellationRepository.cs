


using System.Threading.Tasks;
using dotnetp.DTO;

namespace dotnetp.Service
{
    public interface IBookingCancellationRepository
    {
        Task<int> CreateAsync(BookingCancellationModel bookingCancellation);

        Task<BookingCancellationModel> GetByIdAsync(int id);

        Task UpdateAsync(BookingCancellationModel bookingCancellation);

        Task DeleteAsync(int id);
    }
}
