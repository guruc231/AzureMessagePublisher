using Azure.Messaging.ServiceBus;
using System.Collections.Concurrent;

namespace AzureMessagePublisher
{
    public class ServiceBusClientFactory : IServiceBusClientFactory
    {
        private readonly ConcurrentDictionary<string, ServiceBusClient> _clients = new();
        private readonly IPublisherConfig _config;

        public ServiceBusClientFactory(IPublisherConfig config)
        {
            _config = config;
        }


        /// <summary>
        /// returns a client based on the specified connectionstring
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public ServiceBusClient GetClient(string connectionString)
        {
            if (!_clients.ContainsKey(connectionString))
            {
                if (_clients.TryAdd(connectionString, new ServiceBusClient(connectionString)))
                {
                    throw new Exception("Failed to create servicebus client");
                }
            }
            return _clients[connectionString];
        }



        /// <summary>
        ///  Returns a client based on default connection string
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public ServiceBusClient GetClient()
        {
            if (_clients.TryAdd(_config.ConnectionString, new ServiceBusClient(_config.ConnectionString)))
            {
                throw new Exception("Failed to create servicebus client");
            }
             return _clients[_config.ConnectionString];
        }

    }
}

