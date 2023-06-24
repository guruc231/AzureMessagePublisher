using Azure.Messaging.ServiceBus;

namespace AzureMessagePublisher
{
    public interface IServiceBusClientFactory
    {
        ServiceBusClient GetClient(string connectionString);
        ServiceBusClient GetClient();
    }
}