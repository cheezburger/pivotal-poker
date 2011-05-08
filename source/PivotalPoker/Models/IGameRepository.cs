namespace PivotalPoker.Models
{
    public interface IGameRepository
    {
        Game Get(int storyId);
    }
}