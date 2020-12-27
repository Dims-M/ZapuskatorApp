using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp10.Events
{
    /// <summary>
    /// Базовый Абстрактный класс реализующий интерфейс IEvent
    /// </summary>
    public abstract class BaseEvent : IEvent
    {
        public Guid Id { get; } = Guid.NewGuid();
        public abstract string Type { get; }
        public abstract int Version { get; }
        
        /// <summary>
        /// Отправленный(в работе)
        /// </summary>
        public bool IsSent { get; set; }
        /// <summary>
        /// Доставленный
        /// </summary>
        public bool IsDelivered { get; set; }
        /// <summary>
        /// Количество отправок
        /// </summary>
        public int SentCount { get; set; }
        /// <summary>
        /// Ошибка при отправке
        /// </summary>
        public string SendingError { get; set; }
    }
}
