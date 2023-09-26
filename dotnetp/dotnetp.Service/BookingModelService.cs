using System.Threading.Tasks;
using dotnetp.DataAccess;
using dotnetp.DTO;

namespace dotnetp.Service
{
    public class BookingModelService : IBookingModelService
    {
        private readonly IBookingModelRepository _bookingModelRepository;

        public BookingModelService(IBookingModelRepository bookingModelRepository)
        {
            _bookingModelRepository = bookingModelRepository;
        }

        public async Task<int> CreateBookingAsync(BookingModel booking)
        {
            return await _bookingModelRepository.CreateBookingAsync(booking);
        }

        public async Task<BookingModel> GetBookingByIdAsync(int id)
        {
            return await _bookingModelRepository.GetBookingByIdAsync(id);
        }

        public async Task<bool> UpdateBookingAsync(BookingModel booking)
        {
            return await _bookingModelRepository.UpdateBookingAsync(booking);
        }

        public async Task<bool> DeleteBookingAsync(int id)
        {
            return await _bookingModelRepository.DeleteBookingAsync(id);
        }
    }
}