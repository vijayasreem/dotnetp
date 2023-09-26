using System;
using System.Threading.Tasks;
using dotnetp.DataAccess;
using dotnetp.DTO;

namespace dotnetp.Service
{
    public class BookingModelService : IBookingModelService
    {
        private readonly IBookingModelRepository _repository;

        public BookingModelService(IBookingModelRepository repository)
        {
            _repository = repository;
        }

        public async Task CreateAsync(BookingModel booking)
        {
            // Add your business logic for creating a booking here
            await _repository.CreateAsync(booking);
        }

        public async Task<BookingModel> GetByIdAsync(int id)
        {
            // Add your business logic for getting a booking by ID here
            return await _repository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(BookingModel booking)
        {
            // Add your business logic for updating a booking here
            await _repository.UpdateAsync(booking);
        }

        public async Task DeleteAsync(int id)
        {
            // Add your business logic for deleting a booking here
            await _repository.DeleteAsync(id);
        }

        public async Task<bool> CanCancelBookingAsync(BookingModel booking)
        {
            // Add your business logic for checking if a booking can be cancelled here
            // You can use the booking.CheckInDate property to determine the check-in date
            // and compare it with the current date and time to check if it's within the 24-hour cancellation period
            DateTime currentDateTime = DateTime.Now;
            DateTime cancellationDateTime = booking.CheckInDate.AddHours(-24);

            return currentDateTime < cancellationDateTime;
        }

        public async Task CancelBookingAsync(BookingModel booking)
        {
            // Add your business logic for cancelling a booking here
            // You can call the _repository.DeleteAsync() method to delete the booking from the repository
            await _repository.DeleteAsync(booking.Id);
        }
    }
}