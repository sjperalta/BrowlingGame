using BrowlingGame.BLL.Features.Browling.Dto;
using BrowlingGame.DAL.Context;
using BrowlingGame.BLL.ScoreCalculator;
using Microsoft.EntityFrameworkCore;

namespace BrowlingGame.BLL.Features.Browling
{
    public class BrowlingService : IBrowlingService
    {
        private readonly BrowlingContext _context;
        private readonly IScoreEngine _scoreEngine;

        public BrowlingService(BrowlingContext context, IScoreEngine scoreEngine)
        {
            _context = context;
            _scoreEngine = scoreEngine;
        }

        private Game NewBrowlingGameMap(string playerName) => 
            new()
        {
            Player = new Player()
            {
                PlayerName = playerName
            }
        };

        public async Task<BrowlingOutputDto> Create(CreateBrowlingDto input)
        {
            BrowlingOutputDto output = new() { Success = true };

            if(string.IsNullOrEmpty(input.PlayerName))
                throw new ArgumentNullException(nameof(Create));

            try
            {
                _context.Games.Add(NewBrowlingGameMap(input.PlayerName));
                await _context.SaveChangesAsync();

            }catch (Exception ex) {
                output.Success = false;
                output.Message = "Error trying to create the player";
                output.Exception = ex.Message;
            }

            return output;
        }

        public async Task<int> GetScore(GetScoreDto input)
        {
            var score = await _scoreEngine.GetScore(input.PlayerId);
            return score;
        }

        private Frame newFrameMap(Game? lastGame, SpinDto input, int? framesCount) => new Frame()
        {
            GameId = lastGame.Id,
            Attempt1 = input.FirstAttempt,
            Attempt2 = input.SecondAttempt,
            Attempt3 = input.LastAttempt,
            FrameNumber = framesCount ?? 0
        };

        public async Task<SpinOutputDto> RecordSpin(SpinDto input)
        {
            var output = new SpinOutputDto() { Success = true };
            var player = _context.Players.FirstOrDefault(a => a.Id == input.PlayerId);

            if(player is null)
                throw new ArgumentNullException(nameof(input.PlayerId));

            var lastGame = _context.Games
                .Include(a => a.Frames)
                .Where(a => a.PlayerId == input.PlayerId)
                .OrderBy(a => a.CreatedDate)
                .LastOrDefault();

            if (lastGame is null)
                throw new ArgumentException("No game found");

            var framesCount = lastGame?.Frames.Count;

            if(framesCount >= 10) {
                output.Message = "Game has Ended, games can not exeed 10 frames!";
                output.Success = false;
            }

            try
            {
                var newSpin = newFrameMap(lastGame, input, framesCount);
                _context.Frames.Add(newSpin);
                await _context.SaveChangesAsync();
            }catch(Exception ex)
            {
                output.Message = "Error trying to save the spin!";
                output.Exception = ex.Message;
                output.Success = false;
            }

            return output;
        }
    }
}
