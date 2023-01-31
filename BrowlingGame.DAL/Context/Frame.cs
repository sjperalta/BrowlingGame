using BrowlingGame.DAL.Context.Dto;

namespace BrowlingGame.DAL.Context
{
    public class Frame : Entity<int>, IAudit
    {
        public int GameId { get; set; }
        public Game? Game { get; set; }
        public int FrameNumber { get; set; } = 0;
        public string? Attempt1 { get; set; }
        public string? Attempt2 { get; set; }
        public string? Attempt3 { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime LastModifiedDate { get; set; } = DateTime.Now;
    }
}