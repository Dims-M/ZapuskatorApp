using ConsoleApp10.Events;

namespace ConsoleApp10.MessageBuilders
{
    public interface IMessageBuilderFactory
    {
        IMessageBuilder GetMessageBuilder(IEvent e);
    }
}
