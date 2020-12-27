using System;
using System.Diagnostics;
using System.Threading;
using ConsoleApp10.DataStore;
using ConsoleApp10.MessageBuilders;
using ConsoleApp10.RemoteApi;

namespace ConsoleApp10
{
    class Program
    {
        private static DataSendingManager _manager = new DataSendingManager(new Store(), new MessageBuilderFactory(), new RemoteApiService());
       
        static void Main(string[] args)
        {
            var timejob = new Stopwatch();
            timejob.Start();

            var timer = new Timer(TimerAction, null, TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(10));

            Console.WriteLine("Press any key to stop.");
            Console.ReadKey(true);
            timer.Dispose();
            timejob.Stop();
            Console.WriteLine($"Время работы  приложения = {timejob.Elapsed}");
            Console.ReadKey(true);
        }

        private static void TimerAction(object state)
        {
            _manager.ProcessEvents();
        }
    }
}
