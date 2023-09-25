public interface ICancelBookingService
{
    Task<int> CreateAsync(CancelBookingModel cancelBooking);
    Task<CancelBookingModel> GetByIdAsync(int id);
    Task<bool> UpdateAsync(CancelBookingModel cancelBooking);
    Task<bool> DeleteAsync(int id);
}