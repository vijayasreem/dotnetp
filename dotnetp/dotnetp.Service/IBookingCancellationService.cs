public interface IBookingCancellationService
{
    Task<BookingCancellationModel> GetById(int id);
    Task Create(BookingCancellationModel bookingCancellation);
    Task Update(BookingCancellationModel bookingCancellation);
    Task Delete(int id);
    Task<bool> CanCancelBooking(int bookingId, DateTime checkInDate);
}