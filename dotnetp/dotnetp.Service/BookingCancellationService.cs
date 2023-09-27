using System.Threading.Tasks;
using dotnetp.DataAccess;
using dotnetp.DTO;

namespace dotnetp.Service
{
    public class BookingCancellationService : IBookingCancellationService
    {
        private readonly IBookingCancellationRepository _bookingCancellationRepository;

        public BookingCancellationService(IBookingCancellationRepository bookingCancellationRepository)
        {
            _bookingCancellationRepository = bookingCancellationRepository;
        }

        public async Task<int> CreateAsync(BookingCancellationModel bookingCancellation)
        {
            return await _bookingCancellationRepository.CreateAsync(bookingCancellation);
        }

        public async Task<BookingCancellationModel> GetByIdAsync(int id)
        {
            return await _bookingCancellationRepository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(BookingCancellationModel bookingCancellation)
        {
            await _bookingCancellationRepository.UpdateAsync(bookingCancellation);
        }

        public async Task DeleteAsync(int id)
        {
            await _bookingCancellationRepository.DeleteAsync(id);
        }
    }
}