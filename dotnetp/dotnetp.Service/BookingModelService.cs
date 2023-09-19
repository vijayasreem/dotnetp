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

        public async Task<int> CreateAsync(BookingModel booking)
        {
            // Add your business logic here
            return await _repository.CreateAsync(booking);
        }

        public async Task<BookingModel> GetByIdAsync(int id)
        {
            // Add your business logic here
            return await _repository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(BookingModel booking)
        {
            // Add your business logic here
            await _repository.UpdateAsync(booking);
        }

        public async Task DeleteAsync(int id)
        {
            // Add your business logic here
            await _repository.DeleteAsync(id);
        }
    }
}