namespace BrowlingGame.BLL.Features.Browling.Dto
{
    public class BrowlingOutputDto : IErrorResult
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public string? Exception { get; set; }
    }
}
