using Azure.Messaging.ServiceBus;

namespace AzureMessagePublisher
{
    public interface ISenderFactory
    {
        ServiceBusSender Get(string topicName);
    }
}