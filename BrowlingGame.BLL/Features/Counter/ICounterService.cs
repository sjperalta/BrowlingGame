namespace BrowlingGame.BLL.Features.Counter
{
    public interface ICounterService
    {
        int Get(string routeName);
        void Update(string routeName);
        bool ShouldCache(string routeName);
    }
}