using System;
using ConsoleApp10.MessageBuilders;

namespace ConsoleApp10.RemoteApi
{
    public class RemoteApiService : IRemoteApiService
    {
        private Random _random = new Random(DateTime.Now.Second);

        public ApiResponse Send(IMessage message)
        {
            Console.WriteLine($"API => Send message: {message.Body}");

            var result = _random.Next(0, 2) > 0;

            var response = new ApiResponse
            {
                Result = result,
                ErrorResult = result ? String.Empty : "Api web error"
            };

            Console.WriteLine($"API => GetResponse: Result={response.Result} Error=\"{response.ErrorResult}\"");

            return response;
        }
    }
}
