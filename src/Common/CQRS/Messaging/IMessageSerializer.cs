namespace AGTec.Common.CQRS.Messaging
{
    public interface IMessageSerializer
    {
        byte[] Serialize(IMessage message);
        string ContentType { get; }
        IMessage Deserialize(byte[] messageBody);
    }
}
