using System;

namespace AzureMessagePublisher
{
    public class Message<T>
    {
        public string MessageId { get; } = Guid.NewGuid().ToString();
        public string CorrelationId { get; set; } = "";

        public T Body { get; }

        public string Environment { get; internal set; }

        public Message(T body)
        {
            Body = body;
        }


        internal Message(string messageId, T body)
        {
            MessageId = messageId;
            Body = body;
        }
    }
}
