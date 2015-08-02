using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using QuickFix.Fields;

namespace Ticker
{
    public class FakeTickerClient : ITicker
    {
        private readonly List<string> _crosses;
        private readonly List<decimal> _crossPrice;
        public string Provider { get; set; }
        public int MessagesPerSecond { get; set; }

        public FakeTickerClient(string provider, int messagesPerSecond, List<string> crosses, List<decimal> crossPrice)
        {
            _crosses = crosses;
            _crossPrice = crossPrice;
            Provider = provider;
            MessagesPerSecond = messagesPerSecond;
        }

        public IObservable<Quote> Quotes()
        {
            return Observable.Create<Quote>(observer =>
            {
                Task.Factory.StartNew(() =>
                {
                    var random = new Random();
                    while (true)
                    {
                        var sign = random.Next(-1, 1);
                        var crossIndex = random.Next(0, _crosses.Count);
                        var cross = _crosses[crossIndex];
                        var bid = _crossPrice[crossIndex] + sign * _crossPrice[crossIndex] * 0.001M;
                        var ask = bid*random.Next(90, 98)/100M;
                        _crossPrice[crossIndex] = bid;

                        observer.OnNext(new Quote(Provider, cross, bid, ask, DateTime.Now));

                        Thread.Sleep(TimeSpan.FromMilliseconds(1000/MessagesPerSecond));
                    }
                });

                return Disposable.Empty;
            }
         );
        }

        public void Connect()
        {  
        }

        public void Disconnect()
        {
        }
    }
}
