using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleApp10.Events;

namespace ConsoleApp10.DataStore
{
    /// <summary>
    /// Интерфейс реализующий хранилище.
    /// </summary>
    public interface IStore
    {
        
        /// <summary>
        /// Получение события
        /// </summary>
        /// <returns></returns>
        IEnumerable<IEvent> GetEvents();

        /// <summary>
        /// Боновление данных о событии(нап. было отправлено)
        /// </summary>
        /// <param name="event"></param>
        void UpdateEvent(IEvent @event);
    }

    /// <summary>
    /// Класс реализующий работу тестового БД
    /// </summary>
    public class Store : IStore
    {
        /// <summary>
        /// Лист для хранения данных
        /// </summary>
        private static  readonly List<SomeData> _data = new List<SomeData>();

        /// <summary>
        /// Лист для хранения событий
        /// </summary>
        private static readonly List<IEvent> _events = new List<IEvent>();

        /// <summary>
        /// Конструктор сторе
        /// </summary>
        public Store()
        {

            for (int i = 0; i < 10; i++) //Создаем 10 сущностей
            {

                var dataRecord = new SomeData
                {
                    SomeField = $"Some data..полученные из БД {i}",
                    AnotherSomeField = i
                };

                var eventRecord = new AddDataEvent //Создаем событие.
                {
                    DataKey = dataRecord.Id   //запысываем id(Guid) документа 
                };

                _data.Add(dataRecord);
                _events.Add(eventRecord);
            }
        }

        public IEnumerable<SomeData> GetDataObjects()
        {
            return _data;
        }

        /// <summary>
        /// Получаем  события
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IEvent> GetEvents()
        {
            Console.WriteLine("Get events list:");
            foreach (var eItem in _events)
            {
                Console.WriteLine($"{eItem.Id} -> Sent={eItem.IsSent,-6} Delivered={eItem.IsDelivered,-6}");
            }

            return _events;
        }

        /// <summary>
        /// Обновляем события.
        /// </summary>
        /// <param name="e">Id нужного события</param>
        public void UpdateEvent(IEvent e)
        {
            var eventItem = _events.FirstOrDefault(x => x.Id == e.Id); //дергаем по id из листа с событиями
           
            if (eventItem != null)
            {
                //Обновляем данные о событии
                eventItem.IsDelivered = e.IsDelivered;
                eventItem.IsSent = e.IsSent;
                eventItem.SentCount = e.SentCount;
                eventItem.SendingError = e.SendingError;
            }
        }
    }
}
