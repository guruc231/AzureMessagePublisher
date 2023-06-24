using System;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;

namespace AzureMessagePublisher
{
    internal class MessagePublisher : IMessagePublisher
    {
        private readonly IPublisherConfig config;
        //servicebus client
        private readonly CancellationTokenSource _tokenSource;
        private readonly IMessageSerializer _messageSerializer;
        private readonly ISenderFactory _senderFactory;
        
        public MessagePublisher(IMessageSerializer messageSerializer, ISenderFactory senderFactory)
        {
            _messageSerializer = messageSerializer;
            _senderFactory = senderFactory;
        }

        public async Task PublishAsync<T>(T payload, string topicName)
        {

            //ToDo: validate topic name and payload

            try
            {
                if (payload == null)
                {
                    throw new ArgumentNullException(nameof(payload));
                }

                var sender = _senderFactory.Get(topicName);
                var token = _tokenSource.Token;
                var uniqueId = Guid.NewGuid().ToString();
                var serviceBusMessage = new ServiceBusMessage()
                {
                    Body = new BinaryData(Encoding.UTF8.GetBytes(_messageSerializer.Serialize(payload))),
                    CorrelationId = uniqueId,
                    MessageId = uniqueId
                };
                await sender.SendMessageAsync(serviceBusMessage, token);
            }
            catch (ServiceBusException sbe)
            {
                //handle this
            }
            catch (SerializationException ex)
            {
                //handle this
            }
            catch (Exception ex)
            {
                //handle this
            }

        }
    }
}
