using BrowlingGame.BLL.ScoreCalculator.Dto;

namespace BrowlingGame.BLL.ScoreCalculator.Strategies
{
    public class SpareStrategy : IStrategy<SpareStrategy>, IStrategyBase
    {
        public int Execute(FrameInputDto inputDto)
        {
            return 10 + inputDto.First;
        }
    }
}
