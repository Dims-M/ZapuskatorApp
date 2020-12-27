using ConsoleApp10.Events;

namespace ConsoleApp10.MessageBuilders
{
    public interface IMessageBuilder
    {
        IMessage BuildMessage(IEvent e);
    }
}
