using dotnetp.DataAccess;
using dotnetp.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnetp.Service
{
    public class UserStoryService : IUserStoryService
    {
        private readonly IUserStoryRepository _userStoryRepository;

        public UserStoryService(IUserStoryRepository userStoryRepository)
        {
            _userStoryRepository = userStoryRepository;
        }

        public async Task<UserStoryModel> GetByIdAsync(string id)
        {
            return await _userStoryRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<UserStoryModel>> GetAllAsync()
        {
            return await _userStoryRepository.GetAllAsync();
        }

        public async Task AddAsync(UserStoryModel userStory)
        {
            await _userStoryRepository.AddAsync(userStory);
        }

        public async Task UpdateAsync(UserStoryModel userStory)
        {
            await _userStoryRepository.UpdateAsync(userStory);
        }

        public async Task DeleteAsync(string id)
        {
            await _userStoryRepository.DeleteAsync(id);
        }

        public bool CheckEligibility(UserStoryModel userStory)
        {
            if (userStory.ProductCode != "CTE01")
                return false;

            if (userStory.ProductDescription != "Family Health Insurance")
                return false;

            if (userStory.EffectiveStartDate < new DateTime(2023, 1, 6))
                return false;

            if (userStory.EffectiveEndDate > new DateTime(2027, 7, 12))
                return false;

            if (userStory.BusinessType != "New Business" && userStory.BusinessType != "Renewal")
                return false;

            if (userStory.MinTerm < 1 || userStory.MaxTerm > 3)
                return false;

            if (userStory.NoOfAdults != 1)
                return false;

            if (userStory.NoOfChildren < 1 || userStory.NoOfChildren > 2)
                return false;

            if (userStory.MinAgeAllowed < 0 || userStory.MaxAgeAllowed > 99)
                return false;

            if (userStory.Relationship != "SPOUSE" && userStory.Relationship != "SON" && userStory.Relationship != "SELF" 
                && userStory.Relationship != "MOTHER" && userStory.Relationship != "FATHER" && userStory.Relationship != "DAUGHTER")
                return false;

            if (userStory.Occupation != "Salaried" && userStory.Occupation != "Business" && userStory.Occupation != "Doctor" 
                && userStory.Occupation != "Lawyer" && userStory.Occupation != "Engineer")
                return false;

            if (userStory.RatingCalculator != "25-35" && userStory.RatingCalculator != "35-45")
                return false;

            return true;
        }
    }
}