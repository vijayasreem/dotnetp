public interface ICancelBookingService
{
    Task<CancelBookingModel> GetBookingByIdAsync(int id);
    Task<int> CreateBookingAsync(CancelBookingModel booking);
    Task<bool> UpdateBookingAsync(CancelBookingModel booking);
    Task<bool> DeleteBookingAsync(int id);
    Task<bool> CanCancelBookingAsync(int id);
}