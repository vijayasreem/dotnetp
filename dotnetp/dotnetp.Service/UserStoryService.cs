using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dotnetp.DataAccess;
using dotnetp.DTO;

namespace dotnetp.Service
{
    public class UserStoryService : IUserStoryService
    {
        private readonly IUserStoryRepository _userStoryRepository;

        public UserStoryService(IUserStoryRepository userStoryRepository)
        {
            _userStoryRepository = userStoryRepository;
        }

        public async Task<bool> CheckEligibilityAsync(UserStoryModel userStory)
        {
            // Acceptance Criteria
            if (userStory.ProductCode != "CTE01")
                return false;
            if (userStory.ProductDescription != "Family Health Insurance")
                return false;
            if (userStory.EffectiveStartDate < new DateTime(2023, 01, 06))
                return false;
            if (userStory.EffectiveEndDate > new DateTime(2027, 07, 12))
                return false;
            if (userStory.BusinessType != "New Business" && userStory.BusinessType != "Renewal")
                return false;
            if (userStory.MinTerm != 1 && userStory.MinTerm != 2 && userStory.MinTerm != 3)
                return false;
            if (userStory.MaxTerm != 1 && userStory.MaxTerm != 2 && userStory.MaxTerm != 3)
                return false;
            if (userStory.NumberOfAdults != 1)
                return false;
            if (userStory.NumberOfChildren < 1 || userStory.NumberOfChildren > 2)
                return false;
            if (userStory.MinAgeAllowed < 0 || userStory.MinAgeAllowed > 99)
                return false;
            if (userStory.MaxAgeAllowed < 0 || userStory.MaxAgeAllowed > 99)
                return false;
            if (userStory.Relationship != "SPOUSE" && userStory.Relationship != "SON" && userStory.Relationship != "SELF" &&
                userStory.Relationship != "MOTHER" && userStory.Relationship != "FATHER" && userStory.Relationship != "DAUGHTER")
                return false;
            if (userStory.Occupation != "Salaried" && userStory.Occupation != "Business" && userStory.Occupation != "Doctor" &&
                userStory.Occupation != "Lawyer" && userStory.Occupation != "Engineer")
                return false;
            if (userStory.RatingCalculator != "25-35" && userStory.RatingCalculator != "35-45")
                return false;

            return true;
        }
    }
}