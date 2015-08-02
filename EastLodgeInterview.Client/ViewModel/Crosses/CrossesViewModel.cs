using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using Ticker;

namespace EastLodgeInterview.Client.ViewModel.Crosses
{

	public static class Extension
	{
		public static IObservable<Tuple<Quote, Quote>> BufferMaxAskMinBid<T>(this IObservable<Quote> source, TimeSpan timeSpan)
		{
			return Observable.Create<Tuple<Quote, Quote>>(observer =>
				{
					
					var quoteList = new List<Quote>();

					return source.Subscribe(q =>
						{
							quoteList.Add(q);
							var oldestTime = DateTime.Now.Subtract(timeSpan);
							for (int i = 0; i < quoteList.Count; i++)
							{
								if (quoteList[i].Timestamp < oldestTime)
								{
									quoteList.RemoveAt(i);
								}
							}

							var maxAsk = quoteList.OrderByDescending(quote => quote.Ask).FirstOrDefault();
							var minBid = quoteList.OrderBy(quote => quote.Bid).FirstOrDefault();

							observer.OnNext(new Tuple<Quote, Quote>(minBid, maxAsk));
						});
				});
		}
	}

    class CrossesViewModel : ICrossesViewModel
    {
        public ObservableCollection<ICrossViewModel> Crosses { get; private set; }

        public CrossesViewModel()
        {
            ITicker ticker1 = new FakeTickerClient("JP", 2, new List<string> { "EURUSD", "EURGBP" }, new List<decimal> { 1.23M, 0.78M });
            ITicker ticker2 = new FakeTickerClient("MS", 3, new List<string> { "EURUSD", "EURGBP" }, new List<decimal> { 1.23M, 0.78M });
            ITicker ticker3 = new FakeTickerClient("BA", 2, new List<string> { "EURUSD", "EURGBP" }, new List<decimal> { 1.23M, 0.78M });
            ITicker ticker4 = new FakeTickerClient("JF", 2, new List<string> { "EURUSD", "EURGBP" }, new List<decimal> { 1.23M, 0.78M });

            var quotes = ticker1.Quotes().Merge(ticker2.Quotes()).Merge(ticker3.Quotes()).Merge(ticker4.Quotes());

            Crosses = new ObservableCollection<ICrossViewModel>();
            Crosses.Add(new CrossViewModel("EURUSD", quotes));
            Crosses.Add(new CrossViewModel("EURGBP", quotes));
            Crosses.Add(new CrossViewModel("USDGBP", quotes));
            Crosses.Add(new CrossViewModel("NOKSEK", quotes));
            Crosses.Add(new CrossViewModel("USDCAD", quotes));
        }
    }
}