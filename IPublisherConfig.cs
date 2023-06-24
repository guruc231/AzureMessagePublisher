namespace AzureMessagePublisher
{
    public interface IPublisherConfig
    {
        string ConnectionString { get; }
        string TopicName { get; }
        string Environment { get; }
    }
}
