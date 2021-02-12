using System;
using System.Diagnostics;
using System.Threading;
using ClassLibraryNetFremfork;
using ClassLibraryTelegram;
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
            GetMessage();
        }

        private static void TimerAction(object state)
        {
            _manager.ProcessEvents();
        }


       static void GetMessage()
        {
             Telega telega = new Telega();
             //var message = telega.GetChatTelegram(); //самопистый

            //var message = telega.button3_ClickAsync(); //Полу рабочая версия
            // Console.WriteLine(message);

            var message1 = telega.DownloadFileFromWrongLocationTest();

            Console.WriteLine(message1);
            // Tests tests = new Tests();  //ПРИМЕР с сайта разработчкика
            // tests.AuthUser();


            Console.WriteLine("Press any key to stop.");
            Console.ReadKey(true);
            Console.WriteLine("******************.");

        }

        /// <summary>
        /// Тестоврования время выполнения программы
        /// </summary>
        void TestTime()
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
    }
}
