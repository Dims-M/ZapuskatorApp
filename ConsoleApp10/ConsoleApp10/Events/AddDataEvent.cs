using System;

namespace ConsoleApp10.Events
{
    /// <summary>
    /// Класс реализующий добавление события
    /// </summary>
    public class AddDataEvent : BaseEvent
    {
        /// <summary>
        /// Тип события
        /// </summary>
        public override string Type { get => "AddData"; }

        /// <summary>
        /// Версия
        /// </summary>
        public override int Version { get => 1; }

        /// <summary>
        /// уникальный id документа который сформировал это событие
        /// </summary>
        public Guid DataKey { get; set; }
    }
}
