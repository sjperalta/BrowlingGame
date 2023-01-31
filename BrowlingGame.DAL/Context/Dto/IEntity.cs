namespace BrowlingGame.DAL.Context.Dto
{
    public interface IEntity<TPrimaryKey>
    {
        TPrimaryKey Id { get; set; }
    }
}
