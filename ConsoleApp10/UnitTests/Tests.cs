using ConsoleApp10.Events;
using ConsoleApp10.MessageBuilders;
using Xunit;

namespace UnitTests
{
    public class Tests
    {
        [Fact]
        public void CreateBuilderForKnownEvent()
        {
            // arrange //
            IEvent e = new AddDataEvent();
            IMessageBuilderFactory builderFactory = new MessageBuilderFactory();

            // act //
            var builder = builderFactory.GetMessageBuilder(e);

            // assert // Сравниваем по типу
            Assert.Equal(typeof(AddDataMessageBuilder), builder.GetType());
        }

        [Fact]
        public void CreateBuilderForUnknownEvent()
        {
            // arrange //
            IEvent e = new FakeEvent();
            IMessageBuilderFactory builderFactory = new MessageBuilderFactory();

            // act //
            var builder = builderFactory.GetMessageBuilder(e);

            // assert // Проверрка на null
            Assert.Null(builder);
           // Assert.NotNull(builder);
        }
    }
}
