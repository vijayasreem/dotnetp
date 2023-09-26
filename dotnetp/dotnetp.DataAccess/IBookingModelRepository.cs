
using System.Collections.Generic;
using System.Threading.Tasks;
using dotnetp.DTO;

namespace dotnetp.Service
{
    public interface IBookingModelRepository
    {
        Task<int> CreateAsync(BookingModel booking);
        Task<BookingModel> GetByIdAsync(int id);
        Task<List<BookingModel>> GetAllAsync();
        Task UpdateAsync(BookingModel booking);
        Task DeleteAsync(int id);
    }
}
