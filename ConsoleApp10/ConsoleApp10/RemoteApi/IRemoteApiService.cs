using ConsoleApp10.MessageBuilders;

namespace ConsoleApp10.RemoteApi
{
    /// <summary>
    /// Интерфейс реализующий отправку удаленного API сервиса
    /// </summary>
    public interface IRemoteApiService
    {
        /// <summary>
        /// Отправка сообщения на удаленный Сервер(API)
        /// </summary>
        /// <param name="message"> Само  сообщение </param>
        /// <returns></returns>
        ApiResponse Send(IMessage message);
    }
}
