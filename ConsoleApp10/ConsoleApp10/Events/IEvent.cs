using System;

namespace ConsoleApp10.Events
{
    /// <summary>
    /// Интерфейс реализующий событие
    /// </summary>
    public interface IEvent
    {
        Guid Id { get; }
        
        /// <summary>
        /// Тип сообщений
        /// </summary>
        string Type { get; }
        
        int Version { get; }
        
        /// <summary>
        /// Отправленный(в работе)
        /// </summary>
        bool IsSent { get; set; }

        /// <summary>
        /// Доставленный
        /// </summary>
        bool IsDelivered { get; set; }

        /// <summary>
        /// Количество отправок
        /// </summary>
        int SentCount { get; set; }

        /// <summary>
        /// Ошибка при отправке
        /// </summary>
        string SendingError { get; set; }
    }
}
