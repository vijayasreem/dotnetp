﻿namespace dotnetp
{
    public class BookingModel
    {
        public int Id { get; set; }
        public bool IsCancelled { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CancelationDeadline { get; set; }
    }
}