using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureMessagePublisher
{
    public interface IMessagePublisher
    {
       Task PublishAsync<T>(T payload,string topicName);
    }
}
