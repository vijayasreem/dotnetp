public interface IUserStoryService
{
    Task<UserStoryModel> GetByIdAsync(string id);
    Task<IEnumerable<UserStoryModel>> GetAllAsync();
    Task AddAsync(UserStoryModel userStory);
    Task UpdateAsync(UserStoryModel userStory);
    Task DeleteAsync(string id);
    bool CheckEligibility(UserStoryModel userStory);
}