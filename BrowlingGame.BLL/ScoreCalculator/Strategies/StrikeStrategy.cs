using BrowlingGame.BLL.ScoreCalculator.Dto;

namespace BrowlingGame.BLL.ScoreCalculator.Strategies
{
    public class StrikeStrategy : IStrategy<StrikeStrategy>, IStrategyBase
    {
        public int Execute(FrameInputDto inputDto)
        {
            return 10 + inputDto.First + inputDto.Second;
        }
    }
}