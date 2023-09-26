
using System;

namespace dotnetp
{
    public class BookingCancellationModel
    {
        public int Id { get; set; }
        public DateTime CancelationDate { get; set; }
        public string CustomerId { get; set; }
        public string BookingId { get; set; }
        public string BookingStatus { get; set; }
        public string Email { get; set; }
        public string Reason { get; set; }
    }
}
