using System;

namespace ConsoleApp10.MessageBuilders
{
    public class Message : IMessage
    {
        public Guid Id { get; }
        public string Body { get; }

        public Message(Guid id, string body)
        {
            Body = body;
        }
    }
}
