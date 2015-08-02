using System;

namespace Ticker.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //FakeClientTest
            //ITicker ticker = new FakeTickerClient("JP", 3, new List<string>{ "EURUSD", "EURGBP" }, new List<decimal>{1.23M, 0.78M});
            //ticker.Quotes().Subscribe(OnNext, OnError, OnCompleted);
            //Console.ReadLine();

            ITicker client = null;
            IDisposable subscription = null;

            try
            {
                client = new TickerClient("JP", "client.cfg");
                client.Connect();

                subscription = client.Quotes().Subscribe(quote => Console.WriteLine("OnNext: " + quote));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Console.ReadKey();
            subscription.Dispose();
            Console.ReadKey();
            client.Disconnect();
        }
    }
}
