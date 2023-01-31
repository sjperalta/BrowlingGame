using BrowlingGame.BLL.Features.Browling;
using BrowlingGame.BLL.Features.Browling.Dto;
using BrowlingGame.BLL.ScoreCalculator;
using BrowlingGame.DAL.Context;
using Moq;
using Shouldly;

namespace BrowlingGame.UnitTesting
{
    public class ScoreEngineTest
    {
        protected readonly Mock<BrowlingContext> _dbServiceMock = new();

        [Fact(Skip = "issue with mock db context")]
        public async Task ScoreEngineOpenFrameShouldReturnValidCalculation()
        {
            //Arrange
            Player player = new Player() { Id = 1, PlayerName = "SergioP" };
            var frames = new List<Frame>();
            Frame first = new() { Attempt1 = "9", Attempt2 = "-", Attempt3 = null, GameId = 1, FrameNumber = 0, CreatedDate = DateTime.Now };
            frames.Add(first);
            Game game = new Game() {  Id = 1, Player = player, Frames = frames };     
            _dbServiceMock.Setup(db => db.Games.Add(game)).Returns(It.IsAny<Game>);
            _dbServiceMock.Setup(db => db.Frames.AddRange(frames));
            Mock<IStrategyContext> _strategyContextMock = new();
            Mock<IScoreEngine> _mockScoreEngine = new(_dbServiceMock.Object, _strategyContextMock.Object);
            var engine = _mockScoreEngine.Object;

            //Act
            var result = await engine.GetScore(1);
            _mockScoreEngine.Verify(m => m.GetScore(1), Times.Once());

            //Assert
            result.ShouldBe(9);
        }
    }
}
