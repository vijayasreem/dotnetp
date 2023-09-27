public interface IBookingCancellationService
{
    Task<int> CreateAsync(BookingCancellationModel bookingCancellation);
    Task<BookingCancellationModel> GetByIdAsync(int id);
    Task UpdateAsync(BookingCancellationModel bookingCancellation);
    Task DeleteAsync(int id);
}