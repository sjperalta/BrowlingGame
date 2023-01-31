using BrowlingGame.BLL.Features.Browling;
using BrowlingGame.BLL.Features.Browling.Dto;
using BrowlingGame.BLL.ScoreCalculator;
using BrowlingGame.DAL.Context;
using Moq;
using Shouldly;

namespace BrowlingGame.UnitTesting
{
    public class BrowlingServiceTest
    {
        protected readonly Mock<BrowlingContext> _dbServiceMock = new();
        protected readonly Mock<IBrowlingService> _mockBrowlingService = new();
        protected readonly Mock<IScoreEngine> _mockScoreEngine = new();

        [Fact]
        public async Task CreateGameShouldReturnSuccess()
        {
            //Arrange
            CreateBrowlingDto input = new() { PlayerName = "SergioTest" };
            BrowlingOutputDto output = new() { Success = true };
            _mockBrowlingService.Setup(service => service.Create(It.IsAny<CreateBrowlingDto>()))
                .ReturnsAsync(output);

            //Act
            var service = _mockBrowlingService.Object;
            var result = await service.Create(input);
            _mockBrowlingService.Verify(m => m.Create(input), Times.Once());

            //Assert
            result.Success.ShouldBeTrue();
        }

        [Fact]
        public async Task CreateGameShouldReturnFail()
        {
            //Arrange
            CreateBrowlingDto input = new() { PlayerName = null };
            BrowlingOutputDto output = new() { Success = false };
            _mockBrowlingService.Setup(service => service.Create(It.IsAny<CreateBrowlingDto>()))
                .ReturnsAsync(output);

            //Act
            var service = _mockBrowlingService.Object;
            var result = await service.Create(input);
            _mockBrowlingService.Verify(m => m.Create(input), Times.Once());

            //Assert
            result.Success.ShouldBeFalse();
        }

        [Fact]
        public async Task StoreSpingShouldReturnSuccess()
        {
            //Arrange
            SpinDto input = new() { FirstAttempt = "8", SecondAttempt = "/" };
            SpinOutputDto output = new() { Success = true };
            _mockBrowlingService.Setup(service => service.RecordSpin(It.IsAny<SpinDto>()))
                .ReturnsAsync(output);

            //Act
            var service = _mockBrowlingService.Object;
            var result = await service.RecordSpin(input);
            _mockBrowlingService.Verify(m => m.RecordSpin(input), Times.Once());

            //Assert
            result.Success.ShouldBeTrue();
        }

        [Fact]
        public async Task GetScoreShouldReturnANumber()
        {
            //Arrange
            SpinDto input = new() { FirstAttempt = "8", SecondAttempt = "/" };
            GetScoreDto scoreDto = new() { PlayerId = 1 };
            SpinOutputDto output = new() { Success = true };
            _mockBrowlingService.Setup(service => service.GetScore(It.IsAny<GetScoreDto>()))
                .ReturnsAsync(10);

            //Act
            var service = _mockBrowlingService.Object;
            var result = await service.GetScore(scoreDto);
            _mockBrowlingService.Verify(m => m.GetScore(scoreDto), Times.Once());

            //Assert
            result.ShouldBe(10);
        }
    }
}