using BrowlingGame.BLL.ScoreCalculator.Dto;

namespace BrowlingGame.BLL.ScoreCalculator
{
    public interface IStrategyBase
    {
        int Execute(FrameInputDto inputDto);
    }
}
