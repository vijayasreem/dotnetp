using System;
using System.Threading.Tasks;
using dotnetp.DataAccess;
using dotnetp.DTO;

namespace dotnetp.Service
{
    public class CancelBookingService : ICancelBookingService
    {
        private readonly IBookingDataAccess _bookingDataAccess;

        public CancelBookingService(IBookingDataAccess bookingDataAccess)
        {
            _bookingDataAccess = bookingDataAccess;
        }

        public async Task<CancelBookingModel> GetBookingByIdAsync(int id)
        {
            return await _bookingDataAccess.GetBookingByIdAsync(id);
        }

        public async Task<int> CreateBookingAsync(CancelBookingModel booking)
        {
            return await _bookingDataAccess.CreateBookingAsync(booking);
        }

        public async Task<bool> UpdateBookingAsync(CancelBookingModel booking)
        {
            return await _bookingDataAccess.UpdateBookingAsync(booking);
        }

        public async Task<bool> DeleteBookingAsync(int id)
        {
            return await _bookingDataAccess.DeleteBookingAsync(id);
        }

        public async Task<bool> CanCancelBookingAsync(int id)
        {
            // Check if the booking can be canceled 24 hours prior to check-in
            var booking = await _bookingDataAccess.GetBookingByIdAsync(id);
            var checkInDate = booking.CheckInDate;

            if (DateTime.Now.AddDays(1) <= checkInDate)
            {
                return true;
            }

            return false;
        }
    }
}