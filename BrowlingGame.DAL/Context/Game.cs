using BrowlingGame.DAL.Context.Dto;

namespace BrowlingGame.DAL.Context
{
    public class Game : Entity<int>, IAudit
    {
        public int PlayerId { get; set; }
        public Player? Player { get; set; }
        public List<Frame> Frames { get; set; } = new List<Frame>();
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime LastModifiedDate { get; set; } = DateTime.Now;
    }
}