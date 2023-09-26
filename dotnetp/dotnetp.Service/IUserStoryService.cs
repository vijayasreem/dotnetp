public interface IUserStoryService
{
    Task<bool> CheckEligibilityAsync(UserStoryModel userStory);
}