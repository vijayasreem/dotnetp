public interface IBookingModelService
{
    Task<int> CreateAsync(BookingModel booking);
    Task<BookingModel> GetByIdAsync(int id);
    Task<List<BookingModel>> GetAllAsync();
    Task UpdateAsync(BookingModel booking);
    Task DeleteAsync(int id);
    Task<bool> CanCancelBookingAsync(int id);
    Task CancelBookingAsync(int id);
}