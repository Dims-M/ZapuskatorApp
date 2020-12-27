using System;
using System.Collections.Generic;
using ConsoleApp10.Events;

namespace ConsoleApp10.MessageBuilders
{
    public class MessageBuilderFactory : IMessageBuilderFactory
    {
        private static readonly Dictionary<Tuple<string, int>, IMessageBuilder> _builders = new Dictionary<Tuple<string, int>, IMessageBuilder>
        {
            {new Tuple<string, int>("AddData", 1), new AddDataMessageBuilder()}
        };

        public IMessageBuilder GetMessageBuilder(IEvent e)
        {
            var messageBuilderKey = Tuple.Create(e.Type, e.Version);
            if(_builders.ContainsKey(messageBuilderKey))
            {
                return _builders[messageBuilderKey];
            }

            return null;
        }
    }
}
