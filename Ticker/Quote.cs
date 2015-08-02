using System;

namespace Ticker
{
    public interface IQuote
    {
        string Provider { get; }
        decimal Bid { get; }
        decimal Ask { get; }

        string Cross { get; }
        DateTime Timestamp { get; }

        bool IsStale { get; }
    }

    public class Quote : IQuote
    {
        public string Provider { get; private set; }
        public string Cross { get; private set; }
        public decimal Bid { get; private set; }
        public decimal Ask { get; private set; }
        public DateTime Timestamp { get; private set; }
        public bool IsStale {
            get { return false; }
        }

        public Quote(string provider, string cross, decimal bid, decimal ask, DateTime timestamp)
        {
            Provider = provider;
            Cross = cross;
            Bid = bid;
            Ask = ask;
            Timestamp = timestamp;
        }

        public override string ToString()
        {
            return String.Format("Provider: {0}, Cross: {1}, Bid: {2}, Ask: {3}, Timestamp: {4}", Provider, Cross, Bid, Ask, Timestamp);
        }
    }
}
