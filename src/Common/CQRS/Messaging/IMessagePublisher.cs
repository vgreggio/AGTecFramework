using System.Threading.Tasks;

namespace AGTec.Common.CQRS.Messaging
{
    public interface IMessagePublisher
    {
        Task Publish(string topicName, IMessage message);
    }
}
