using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp10.Events;

namespace UnitTests
{
    /// <summary>
    /// Тестовой класс описывающий события.
    /// </summary>
    public class FakeEvent : BaseEvent
    {
        public override string Type { get => "Fake"; }
        public override int Version { get => 2; }
    }
}
