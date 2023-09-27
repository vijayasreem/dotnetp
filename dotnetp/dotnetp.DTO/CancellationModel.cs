
using System;

namespace dotnetp
{
    public class CancellationModel
    {
        public int Id { get; set; }
        public DateTime CancellationDate { get; set; }
        public int BookingId { get; set; }
        public string CustomerEmail { get; set; }
        public string Reason { get; set; }
    }
}
