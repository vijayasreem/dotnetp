namespace dotnetp
{
    public class BookingCancellationModel
    {
        public int Id { get; set; }
        
        public DateTime CheckInDate { get; set; }
        
        public DateTime CancellationDate { get; set; }
    }
}