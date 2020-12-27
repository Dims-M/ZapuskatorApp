using ConsoleApp10.Events;

namespace ConsoleApp10.MessageBuilders
{
    public class AddDataMessageBuilder : IMessageBuilder
    {
        public IMessage BuildMessage(IEvent e)
        {
            var addDataEvent = e as AddDataEvent;

            // todo: Здесь должна происходить подготовка сообщения. Получение данных сообщения из БД и т.д.

            return new Message(e.Id,$"<Message Type=\"{e.Type}\" Id=\"{e.Id}\"><ObjId>{addDataEvent.DataKey}<ObjId/></Message>");
        }
    }
}
