using dotnetp.DataAccess;
using dotnetp.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnetp.Service
{
    public class BookingModelService : IBookingModelService
    {
        private readonly IBookingModelRepository _bookingModelRepository;

        public BookingModelService(IBookingModelRepository bookingModelRepository)
        {
            _bookingModelRepository = bookingModelRepository;
        }

        public async Task<int> CreateAsync(BookingModel booking)
        {
            // TODO: Add your implementation here
            return await _bookingModelRepository.CreateAsync(booking);
        }

        public async Task<BookingModel> GetByIdAsync(int id)
        {
            // TODO: Add your implementation here
            return await _bookingModelRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<BookingModel>> GetAllAsync()
        {
            // TODO: Add your implementation here
            return await _bookingModelRepository.GetAllAsync();
        }

        public async Task<bool> UpdateAsync(BookingModel booking)
        {
            // TODO: Add your implementation here
            return await _bookingModelRepository.UpdateAsync(booking);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            // TODO: Add your implementation here
            return await _bookingModelRepository.DeleteAsync(id);
        }

        // Additional methods related to the cancellation of booking

        public async Task<bool> CancelBookingAsync(int id)
        {
            BookingModel booking = await _bookingModelRepository.GetByIdAsync(id);

            if (booking != null)
            {
                DateTime checkInDate = booking.CheckInDate;
                DateTime cancelationDeadline = checkInDate.AddHours(-24);

                if (DateTime.Now < cancelationDeadline)
                {
                    // TODO: Implement the cancellation logic
                    // Update the booking status, calculate refund, etc.
                    return true;
                }
            }

            return false;
        }
    }
}