using BrowlingGame.DAL.Context.Dto;

namespace BrowlingGame.DAL.Context
{
    public class Player : Entity<int>
    {
        public string? PlayerName { get; set; }
    }
}
