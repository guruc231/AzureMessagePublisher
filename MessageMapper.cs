using System;
using System.Text;
using Azure.Messaging.ServiceBus;

namespace AzureMessagePublisher
{
    internal interface IMessageMapper
    {
        ServiceBusMessage ToServiceBusMessage<T>(Message<T> message);
    }

    internal class MessageMapper : IMessageMapper
    {
        private readonly IMessageSerializer serializer;

        public MessageMapper(IMessageSerializer serializer)
        {
            this.serializer = serializer;
        }

        public ServiceBusMessage ToServiceBusMessage<T>(Message<T> message)
        {
            var payload = new Payload<T> {
                Body = message.Body,
                Environment = message.Environment
            };
            var bytes = Encoding.UTF8.GetBytes(serializer.Serialize(payload));
            return new ServiceBusMessage(bytes)
            {
                MessageId = message.MessageId,
                CorrelationId = message.CorrelationId
            };
        }

    }

    internal class Payload<T>
    {
        public T Body { get; set; }
        public DateTimeOffset PublishedAt { get; set; }
        public DateTimeOffset EnqueuedAt { get; set; }
        public string Environment { get; set; }
    }

}
