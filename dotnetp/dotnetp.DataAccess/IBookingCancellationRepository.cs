interface IBookingCancellationRepository
{
    Task<BookingCancellationModel> GetById(int id);
    Task Create(BookingCancellationModel bookingCancellation);
    Task Update(BookingCancellationModel bookingCancellation);
    Task Delete(int id);
}