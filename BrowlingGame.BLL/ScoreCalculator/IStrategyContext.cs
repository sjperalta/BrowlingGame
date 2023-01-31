using BrowlingGame.BLL.ScoreCalculator.Dto;

namespace BrowlingGame.BLL.ScoreCalculator
{
    public interface IStrategyContext
    {
        public void SetStrategy(IStrategyBase strategy);
        public int CalculateScore(FrameInputDto inputDto);
    }
}
