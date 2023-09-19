namespace dotnetp
{
    public class EndorsementModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string MakerActivity { get; set; }
        public string CheckerActivity { get; set; }
        public DateTime Timestamp { get; set; }
        public string UserId { get; set; }
        public string RelevantDetails { get; set; }
    }
}