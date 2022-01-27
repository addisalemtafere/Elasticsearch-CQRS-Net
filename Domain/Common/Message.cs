namespace Domain.Common
{
    public abstract class Message : IMessage<Guid>
    {
        public Guid Id { get; set; }

        public Message()
        {
            Id = Guid.NewGuid();
        }
    }
}