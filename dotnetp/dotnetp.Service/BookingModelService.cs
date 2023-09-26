
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dotnetp.DataAccess;
using dotnetp.DTO;

namespace dotnetp.Service
{
    public class BookingModelService
    {
        private readonly IBookingModelRepository _bookingRepository;

        public BookingModelService(IBookingModelRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<int> CreateAsync(BookingModel booking)
        {
            // Add your business logic here
            return await _bookingRepository.CreateAsync(booking);
        }

        public async Task<BookingModel> GetByIdAsync(int id)
        {
            // Add your business logic here
            return await _bookingRepository.GetByIdAsync(id);
        }

        public async Task<List<BookingModel>> GetAllAsync()
        {
            // Add your business logic here
            return await _bookingRepository.GetAllAsync();
        }

        public async Task UpdateAsync(BookingModel booking)
        {
            // Add your business logic here
            await _bookingRepository.UpdateAsync(booking);
        }

        public async Task DeleteAsync(int id)
        {
            // Add your business logic here
            await _bookingRepository.DeleteAsync(id);
        }

        public async Task<bool> CanCancelBookingAsync(int id)
        {
            // Get the booking by id
            BookingModel booking = await GetByIdAsync(id);

            // Check if the booking is within the cancellation window
            DateTime checkInDate = booking.CheckInDate;
            TimeSpan timeUntilCheckIn = checkInDate - DateTime.Now;
            bool isWithinCancellationWindow = timeUntilCheckIn.TotalHours > 24;

            return isWithinCancellationWindow;
        }

        public async Task CancelBookingAsync(int id)
        {
            // Check if the booking can be canceled
            bool canCancelBooking = await CanCancelBookingAsync(id);

            if (canCancelBooking)
            {
                // Perform the cancellation logic here

                // Delete the booking
                await DeleteAsync(id);
            }
            else
            {
                throw new Exception("Cannot cancel the booking as it is within 24 hours of check-in");
            }
        }
    }
}
