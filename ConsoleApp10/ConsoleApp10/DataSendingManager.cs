using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using ConsoleApp10.DataStore;
using ConsoleApp10.Events;
using ConsoleApp10.MessageBuilders;
using ConsoleApp10.RemoteApi;

namespace ConsoleApp10
{
    public class DataSendingManager
    {

        private readonly IStore _store;
        private readonly IMessageBuilderFactory _builderFactory;
        private readonly IRemoteApiService _apiService;


        public DataSendingManager(IStore store, IMessageBuilderFactory messageBuilderFactory, IRemoteApiService apiService)
        {
            _store = store;
            _builderFactory = messageBuilderFactory;
            _apiService = apiService;
        }

        public void ProcessEvents()
        {
            var events = GetEvents();
            
            foreach (var e in events)
            {
                var message = ConstructMessage(e);
                if (message != null)
                {
                    var result = SendMessage(message);

                    SetEventResult(e, result);
                }
            }
        }

        /// <summary>
        /// Получаем событие
        /// </summary>
        /// <returns></returns>
        private IEnumerable<IEvent> GetEvents()
        {
            var events = _store.GetEvents().Where(x => !x.IsSent).ToList();

            MarkEventsAsSent(events);

            return events;
        }

        /// <summary>
        /// Ставим метку что событие было отправлено
        /// </summary>
        /// <param name="events"></param>
        private void MarkEventsAsSent(IEnumerable<IEvent> events)
        {
            foreach (var e in events)
            {
                e.IsSent = true;
                WriteEventToStore(e);
            }
        }

        private IMessage ConstructMessage(IEvent e)
        {
            var builder = _builderFactory.GetMessageBuilder(e);
            return builder?.BuildMessage(e);
        }

        private ApiResponse SendMessage(IMessage message)
        {
            return _apiService.Send(message);
        }

        private void SetEventResult(IEvent e, ApiResponse response)
        {
            e.SentCount++;

            if (response.Result)
            {
                e.IsDelivered = true;
            }
            else
            {
                e.IsSent = false;
                e.SendingError = response.ErrorResult;
            }

            WriteEventToStore(e);
        }

        /// <summary>
        /// Запись данных об изменении статуса события.
        /// </summary>
        /// <param name="e">Сам обект события</param>
        private void WriteEventToStore(IEvent e)
        {
            _store.UpdateEvent(e);
        }
    }
}
