


using System.Threading.Tasks;
using dotnetp.DTO;

namespace dotnetp.Service
{
    public interface IBookingModelRepository
    {
        Task<int> CreateBookingAsync(BookingModel booking);
        Task<BookingModel> GetBookingByIdAsync(int id);
        Task<bool> UpdateBookingAsync(BookingModel booking);
        Task<bool> DeleteBookingAsync(int id);
    }
}
