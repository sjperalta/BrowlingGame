namespace BrowlingGame.DAL.Context.Dto
{
    public interface IAudit
    {
        DateTime CreatedDate { get; set; }
        DateTime LastModifiedDate { get; set; }
    }
}
