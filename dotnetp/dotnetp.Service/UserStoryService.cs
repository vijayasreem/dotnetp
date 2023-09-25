using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dotnetp.DTO;
using dotnetp.DataAccess;

namespace dotnetp.Service
{
    public class UserStoryService : IUserStoryService
    {
        private readonly IUserStoryModelRepository _userStoryRepository;

        public UserStoryService(IUserStoryModelRepository userStoryRepository)
        {
            _userStoryRepository = userStoryRepository;
        }

        public async Task<bool> CheckEligibilityAsync(UserStoryModel userStoryModel)
        {
            if (userStoryModel.ProductCode != "CTE01")
                return false;

            if (userStoryModel.ProductDescription != "Family Health Insurance")
                return false;

            if (userStoryModel.EffectiveStartDate < new DateTime(2023, 1, 6))
                return false;

            if (userStoryModel.EffectiveEndDate > new DateTime(2027, 7, 12))
                return false;

            if (userStoryModel.BusinessType != "New Business" && userStoryModel.BusinessType != "Renewal")
                return false;

            if (userStoryModel.MinTerm < 1 || userStoryModel.MaxTerm > 3)
                return false;

            if (userStoryModel.NumberOfAdults != 1)
                return false;

            if (userStoryModel.NumberOfChildren < 1 || userStoryModel.NumberOfChildren > 2)
                return false;

            if (userStoryModel.MinAgeAllowed < 0 || userStoryModel.MaxAgeAllowed > 99)
                return false;

            if (userStoryModel.Relationship != "SPOUSE" && userStoryModel.Relationship != "SON" && userStoryModel.Relationship != "SELF" && userStoryModel.Relationship != "MOTHER" && userStoryModel.Relationship != "FATHER" && userStoryModel.Relationship != "DAUGHTER")
                return false;

            if (userStoryModel.Occupation != "Salaried" && userStoryModel.Occupation != "Business" && userStoryModel.Occupation != "Doctor" && userStoryModel.Occupation != "Lawyer" && userStoryModel.Occupation != "Engineer")
                return false;

            if (userStoryModel.RatingCalculator != "25-35" && userStoryModel.RatingCalculator != "35-45")
                return false;

            return true;
        }
    }
}