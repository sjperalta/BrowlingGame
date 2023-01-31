namespace BrowlingGame.BLL.Features.Browling.Dto
{
    public interface IErrorResult
    {
        public string? Message { get; set; }
        public string? Exception { get; set; }
    }
}
