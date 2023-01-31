using BrowlingGame.BLL.Features.Browling;
using BrowlingGame.BLL.Features.Browling.Dto;
using BrowlingGame.BLL.Features.Counter;
using LazyCache;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BrowlingGame.Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BrowlingGameController : ControllerBase
    {
        private readonly IBrowlingService _browlingService;
        private readonly ILogger<BrowlingGameController> _logger;
        private readonly IAppCache _cache;
        private readonly ICounterService _counterService;

        public BrowlingGameController(IBrowlingService browlingService, 
            ILogger<BrowlingGameController> logger,
            IAppCache cache,
            ICounterService counterService) {
            _browlingService = browlingService;
            _logger = logger;
            _cache = cache;
            _counterService = counterService;
        }

        // GET Score/5
        [HttpGet("Score/{playerId}")]
        public async Task<int> GetScore(int playerId)
        {
            _logger.LogInformation($"Getting Store for player {playerId}");
            _counterService.Update(nameof(GetScore));
            int result = 0;

            if (_counterService.ShouldCache(nameof(GetScore)))
            {
                _logger.LogInformation($"Route is cached: {nameof(GetScore)}");
                result = await _cache.GetOrAddAsync("get-score", 
                   () => _browlingService.GetScore(new GetScoreDto { PlayerId = playerId }));
            }
            else
            {
                _logger.LogInformation($"not cached yet: {nameof(GetScore)}");
                result = await _browlingService.GetScore(new GetScoreDto { PlayerId = playerId });
            }

            return result;
        }

        // POST Create
        [HttpPost("Create")]
        public async Task<BrowlingOutputDto> Post([FromBody] CreateBrowlingDto input)
        {
            _logger.LogInformation($"Creating a new game for PlayerName: {input.PlayerName}");
            return await _browlingService.Create(input);
        }

        // POST RecordSpin
        [HttpPost("RecordSpin")]
        public async Task<SpinOutputDto> PostSpin([FromBody] SpinDto input)
        {
            _logger.LogInformation($"Creating a new sping for PlayerId: {input.PlayerId}");
            return await _browlingService.RecordSpin(input);
        }
    }
}
