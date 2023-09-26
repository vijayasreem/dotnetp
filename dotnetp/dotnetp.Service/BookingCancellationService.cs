using dotnetp.DataAccess;
using dotnetp.DTO;
using System;
using System.Threading.Tasks;

namespace dotnetp.Service
{
    public class BookingCancellationService : IBookingCancellationService
    {
        private readonly IBookingCancellationRepository _bookingCancellationRepository;

        public BookingCancellationService(IBookingCancellationRepository bookingCancellationRepository)
        {
            _bookingCancellationRepository = bookingCancellationRepository;
        }

        public async Task<BookingCancellationModel> GetById(int id)
        {
            return await _bookingCancellationRepository.GetById(id);
        }

        public async Task Create(BookingCancellationModel bookingCancellation)
        {
            await _bookingCancellationRepository.Create(bookingCancellation);
        }

        public async Task Update(BookingCancellationModel bookingCancellation)
        {
            await _bookingCancellationRepository.Update(bookingCancellation);
        }

        public async Task Delete(int id)
        {
            await _bookingCancellationRepository.Delete(id);
        }

        public async Task<bool> CanCancelBooking(int bookingId, DateTime checkInDate)
        {
            TimeSpan timeDifference = checkInDate - DateTime.Now;

            if (timeDifference.TotalHours >= 24)
            {
                BookingCancellationModel bookingCancellation = await _bookingCancellationRepository.GetById(bookingId);

                if (bookingCancellation == null)
                {
                    // Update booking status to "canceled"
                    bookingCancellation = await _bookingCancellationRepository.GetById(bookingId);
                    bookingCancellation.Status = "canceled";
                    await _bookingCancellationRepository.Update(bookingCancellation);

                    // Send confirmation email to the customer
                    await SendCancellationConfirmationEmail(bookingCancellation);

                    return true;
                }
                else
                {
                    throw new Exception("Booking has already been canceled.");
                }
            }
            else
            {
                throw new Exception("Cannot cancel booking less than 24 hours before check-in.");
            }
        }

        private async Task SendCancellationConfirmationEmail(BookingCancellationModel bookingCancellation)
        {
            // Code to send cancellation confirmation email
        }
    }
}