using System.Threading.Tasks;
using dotnetp.DataAccess;
using dotnetp.DTO;

namespace dotnetp.Service
{
    public class CancelBookingService : ICancelBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IEmailService _emailService;

        public CancelBookingService(IBookingRepository bookingRepository, IEmailService emailService)
        {
            _bookingRepository = bookingRepository;
            _emailService = emailService;
        }

        public async Task<int> CreateAsync(CancelBookingModel cancelBooking)
        {
            return await _bookingRepository.CreateAsync(cancelBooking);
        }

        public async Task<CancelBookingModel> GetByIdAsync(int id)
        {
            return await _bookingRepository.GetByIdAsync(id);
        }

        public async Task<bool> UpdateAsync(CancelBookingModel cancelBooking)
        {
            var booking = await _bookingRepository.GetByIdAsync(cancelBooking.Id);

            if (booking == null)
            {
                return false;
            }

            if (booking.CheckInDate.Subtract(DateTime.Now).TotalHours <= 24)
            {
                return false;
            }

            booking.Status = "canceled";
            var isUpdated = await _bookingRepository.UpdateAsync(booking);

            if (isUpdated)
            {
                await _emailService.SendCancellationEmail(booking.CustomerEmail, booking);
            }

            return isUpdated;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var booking = await _bookingRepository.GetByIdAsync(id);

            if (booking == null)
            {
                return false;
            }

            if (booking.CheckInDate.Subtract(DateTime.Now).TotalHours <= 24)
            {
                return false;
            }

            var isDeleted = await _bookingRepository.DeleteAsync(id);

            if (isDeleted)
            {
                await _emailService.SendCancellationEmail(booking.CustomerEmail, booking);
            }

            return isDeleted;
        }
    }
}