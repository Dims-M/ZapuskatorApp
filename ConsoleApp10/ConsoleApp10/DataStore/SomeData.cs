using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp10.DataStore
{
    /// <summary>
    /// Класс реализующий отправку данных
    /// </summary>
    public class SomeData
    {
       
        public Guid Id { get; } = Guid.NewGuid();

        /// <summary>
        /// Поле с  тестовыми данными...
        /// </summary>
        public string SomeField { get; set; }

        /// <summary>
        /// Поле с  тестовыми данными...*****.....
        /// </summary>
        public int AnotherSomeField { get; set; }
    }
}
