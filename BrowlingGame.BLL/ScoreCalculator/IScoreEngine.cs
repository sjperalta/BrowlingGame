namespace BrowlingGame.BLL.ScoreCalculator
{
    public interface IScoreEngine
    {
        Task<int> GetScore(int playerId);
    }
}
