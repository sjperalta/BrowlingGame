using BrowlingGame.BLL.ScoreCalculator.Dto;

namespace BrowlingGame.BLL.ScoreCalculator.Strategies
{
    public class OpenFrameStrategy : IStrategy<OpenFrameStrategy>, IStrategyBase
    {
        public int Execute(FrameInputDto inputDto)
        {
            return inputDto.Attemp1 + inputDto.Attemp2 + inputDto.Attemp3;
        }
    }
}
