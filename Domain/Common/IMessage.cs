namespace Domain.Common
{
    public interface IMessage<TPrimaryKey>
    {
        TPrimaryKey Id { get; set; }
    }
}