using BrowlingGame.BLL.ScoreCalculator.Dto;
using BrowlingGame.BLL.ScoreCalculator.Strategies;
using BrowlingGame.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace BrowlingGame.BLL.ScoreCalculator
{
    public class ScoreEngine: IScoreEngine
    {
        private readonly BrowlingContext _context;
        private readonly IStrategyContext _strategyContext;

        public ScoreEngine(BrowlingContext context, IStrategyContext strategyContext)
        {
            _context = context;
            _strategyContext = strategyContext;
        }

        private int GetNumber(string? symbol) => symbol switch
        {
            "x" => 10,
            "-" => 0,
            "/" => 10,
            null => 0,
            _ => int.Parse(symbol),
        };

        private IStrategyBase ChooseOperation(string? firstAttempt, string? secondAttempt)
        {
            if (firstAttempt is null || secondAttempt is null)
                throw new ArgumentException(nameof(ChooseOperation));

            List<string> attempts = new() { firstAttempt, secondAttempt };
            if (attempts.Any(number => number == "x"))
                return new StrikeStrategy();
            else if (attempts.Any(number => number == "/"))
                return new SpareStrategy();
            else
                return new OpenFrameStrategy();
        }

        private FrameInputDto ParseScore(List<Frame> frames, int currentFrame)
        {
            var current = frames.FirstOrDefault(a => a.FrameNumber == currentFrame);
   
            if(current == null)
            {
                throw new ArgumentNullException(nameof(ParseScore));
            }

            var score = new FrameInputDto();

            score.Attemp1 = GetNumber(current.Attempt1?.ToLower());
            score.Attemp2 = GetNumber(current.Attempt2?.ToLower());
            score.Attemp3 = GetNumber(current.Attempt3?.ToLower());
            var counter = currentFrame;
            counter++;
            var next = frames.FirstOrDefault(a => a.FrameNumber == counter);
            score.First = GetNumber(next?.Attempt1?.ToLower());
            counter++;
            var nextRound = frames.FirstOrDefault(a => a.FrameNumber == counter);
            score.Second = GetNumber(nextRound?.Attempt1?.ToLower());
            score.Strategy = ChooseOperation(current.Attempt1, current.Attempt2);
            
            return score;
        }
        public async Task<int> GetScore(int playerId)
        {
            var game = await _context
                .Games
                .Include(a => a.Frames)
                .Where(a => a.PlayerId == playerId)
                .FirstOrDefaultAsync();

            var frames = game?.Frames
                .OrderBy(a => a.FrameNumber)
                .Select(a => ParseScore(game.Frames, a.FrameNumber));

            var scores = frames?
                .Select(frame => _strategyContext.CalculateScore(frame));           

            return scores?.Sum() ?? 0;
        }
    }
}
