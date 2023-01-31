using BrowlingGame.BLL.ScoreCalculator.Dto;

namespace BrowlingGame.BLL.ScoreCalculator
{
    public class StrategyContext: IStrategyContext
    {
        private IStrategyBase? _strategy;

        public StrategyContext() {
        }
        public void SetStrategy(IStrategyBase strategy)
        {
            _strategy = strategy;
        }

        public int CalculateScore(FrameInputDto inputDto)
        {
            if (inputDto.Strategy == null)
                throw new ArgumentNullException(nameof(CalculateScore));

            _strategy = inputDto.Strategy;
            var result = _strategy.Execute(inputDto);
            return result;
        }
    }
}
