using System.Reactive.Disposables;
using System.Reactive.Linq;
using QuickFix;
using System;

namespace Ticker
{
    public class TickerClient : IApplication, ITicker
    {
        private readonly string _lProvider;
        private readonly IInitiator _initiator;

        private event Action<Quote> QuoteReceived;

        private IObservable<Quote> quotes;
        public TickerClient(string lProvider, string configFile)
        {
            _lProvider = lProvider;
            var settings = new SessionSettings(configFile);
            IMessageStoreFactory storeFactory = new FileStoreFactory(settings);
            ILogFactory logFactory = new FileLogFactory(settings);
            _initiator = new QuickFix.Transport.SocketInitiator(this, storeFactory, settings, logFactory);
        }

        private void Start()
        {
            _initiator.Start();
        }

        private void Stop()
        {
            _initiator.Stop();
        }

        public void FromAdmin(Message message, SessionID sessionID)
        {
            Console.WriteLine("FromAdmin: " + message);
        }

        public void FromApp(Message message, SessionID sessionID)
        {
            var handler = QuoteReceived;
            if (handler != null)
            {
                handler(new Quote(_lProvider, message.GetString(55), message.GetDecimal(1), message.GetDecimal(2), message.Header.GetDateTime(52)));
            }

            Console.WriteLine("FromApp: " + message);
        }

        public void OnCreate(SessionID sessionID)
        {
            Console.WriteLine("OnCreate: " + sessionID);
        }

        public void OnLogon(SessionID sessionID)
        {
            Console.WriteLine("OnLogon: " + sessionID);
        }

        public void OnLogout(SessionID sessionID)
        {
            Console.WriteLine("OnLogout: " + sessionID);
        }

        public void ToAdmin(Message message, SessionID sessionID)
        {
            Console.WriteLine("ToAdmin: " + message);
        }

        public void ToApp(Message message, SessionID sessionId)
        {
            Console.WriteLine("ToApp: " + message);
        }

        public IObservable<Quote> Quotes()
        {
            return Observable.Create<Quote>(observer =>
                    {
                        QuoteReceived += observer.OnNext;
                        return Disposable.Create(() =>QuoteReceived -= observer.OnNext);
                    }
                );
        }

        public void Connect()
        {
            Start();
        }

        public void Disconnect()
        {
            Stop();
        }
    }
}
