using BrowlingGame.BLL.Features.Browling.Dto;

namespace BrowlingGame.BLL.Features.Browling
{
    public interface IBrowlingService
    {
        public Task<BrowlingOutputDto> Create(CreateBrowlingDto input);
        public Task<SpinOutputDto> RecordSpin(SpinDto input);
        public Task<int> GetScore(GetScoreDto input);
    }
}
