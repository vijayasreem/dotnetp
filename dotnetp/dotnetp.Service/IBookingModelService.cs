public interface IBookingModelService
{
    Task CreateAsync(BookingModel booking);
    Task<BookingModel> GetByIdAsync(int id);
    Task UpdateAsync(BookingModel booking);
    Task DeleteAsync(int id);
    Task<bool> CanCancelBookingAsync(BookingModel booking);
    Task CancelBookingAsync(BookingModel booking);
}