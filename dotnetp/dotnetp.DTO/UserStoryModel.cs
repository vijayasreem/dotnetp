
using System;

namespace dotnetp
{
    public class UserStoryModel
    {
        public string Id { get; set; }
        public string ProductCode { get; set; }
        public string ProductDescription { get; set; }
        public DateTime EffectiveStartDate { get; set; }
        public DateTime EffectiveEndDate { get; set; }
        public string BusinessType { get; set; }
        public int MinTerm { get; set; }
        public int MaxTerm { get; set; }
        public int NumberOfAdults { get; set; }
        public int NumberOfChildren { get; set; }
        public int MinAgeAllowed { get; set; }
        public int MaxAgeAllowed { get; set; }
        public string Relationship { get; set; }
        public string Occupation { get; set; }
        public string RatingCalculator { get; set; }
    }
}
