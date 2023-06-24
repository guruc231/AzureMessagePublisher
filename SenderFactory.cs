using System.Collections.Concurrent;
using Azure.Messaging.ServiceBus;

namespace AzureMessagePublisher
{
    public class SenderFactory : ISenderFactory
    {
        private readonly ConcurrentDictionary<string, ServiceBusSender> _senders = new();
        private IServiceBusClientFactory _clients;

        public SenderFactory(IServiceBusClientFactory clients)
        {
            _clients = clients;
        }

        public ServiceBusSender Get(string topicName)
        {
            if (!_senders.ContainsKey(topicName))
            {
                var client = _clients.GetClient();
                if (_senders.TryAdd(topicName, client.CreateSender(topicName)))
                {
                    throw new Exception("Failed to create Sender");
                }
            }
            return _senders[topicName];
        }
    }
}
