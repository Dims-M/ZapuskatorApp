using System;

namespace ConsoleApp10.MessageBuilders
{
    public interface IMessage
    {
        Guid Id { get; }
        string Body { get; }
    }
}
