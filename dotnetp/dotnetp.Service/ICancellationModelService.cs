public interface ICancellationModelService
{
    Task<int> CreateAsync(CancellationModel cancellation);
    Task<CancellationModel> GetByIdAsync(int id);
    Task<List<CancellationModel>> GetAllAsync();
    Task<bool> UpdateAsync(CancellationModel cancellation);
    Task<bool> DeleteAsync(int id);
    Task<bool> CancelBookingAsync(int bookingId);
}