namespace PivotalPoker.Models
{
    public interface IGameRepository
    {
        Game Get(int projectId, int storyId);
    }
}