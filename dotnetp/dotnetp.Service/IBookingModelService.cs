using System.Threading.Tasks;
using dotnetp.DTO;

namespace dotnetp.Service
{
    public interface IBookingModelService
    {
        Task<int> CreateAsync(BookingModel booking);
        Task<BookingModel> GetByIdAsync(int id);
        Task UpdateAsync(BookingModel booking);
        Task DeleteAsync(int id);
    }
}