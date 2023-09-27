using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnetp.Service
{
    public class CancellationModelService : ICancellationModelService
    {
        private readonly ICancellationModelRepository _repository;

        public CancellationModelService(ICancellationModelRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> CreateAsync(CancellationModel cancellation)
        {
            // TODO: Implement logic to create a cancellation model
            return await _repository.CreateAsync(cancellation);
        }

        public async Task<CancellationModel> GetByIdAsync(int id)
        {
            // TODO: Implement logic to retrieve a cancellation model by id
            return await _repository.GetByIdAsync(id);
        }

        public async Task<List<CancellationModel>> GetAllAsync()
        {
            // TODO: Implement logic to retrieve all cancellation models
            return await _repository.GetAllAsync();
        }

        public async Task<bool> UpdateAsync(CancellationModel cancellation)
        {
            // TODO: Implement logic to update a cancellation model
            return await _repository.UpdateAsync(cancellation);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            // TODO: Implement logic to delete a cancellation model
            return await _repository.DeleteAsync(id);
        }

        public async Task<bool> CancelBookingAsync(int bookingId)
        {
            // TODO: Implement logic to cancel a booking
            var booking = await _repository.GetByIdAsync(bookingId);

            if (booking == null)
            {
                throw new ArgumentException("Booking not found");
            }

            if (booking.CheckInDate.Subtract(DateTime.Now).TotalHours < 24)
            {
                throw new InvalidOperationException("Cannot cancel booking less than 24 hours before check-in");
            }

            booking.Status = "Canceled";
            await _repository.UpdateAsync(booking);

            // TODO: Send confirmation email to the customer

            return true;
        }
    }
}