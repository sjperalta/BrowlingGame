using BrowlingGame.BLL.ScoreCalculator.Dto;

namespace BrowlingGame.BLL.ScoreCalculator
{
    public interface IStrategy<T> where T : class, IStrategy<T>
    {
        int Execute(FrameInputDto inputDto);
    }
}
