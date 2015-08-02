using System.Reactive.Linq;
using PropertyChanged;
using Ticker;
using System;

namespace EastLodgeInterview.Client.ViewModel.Crosses
{
    [ImplementPropertyChanged]
    public class CrossViewModel : NotifyPropertyChanged, ICrossViewModel
    {
        private readonly ITicker _ticker;
        public string Cross { get; private set; }
        public decimal Ask { get; private set; }
        public decimal Bid { get; private set; }
        public decimal Spread
        {
            get { return (Bid - Ask)*100; }
        }
        public string AskProvider { get; private set; }
        public string BidProvider { get; private set; }

        public PriceMovement AskMovement { get; private set; }
        public PriceMovement BidMovement { get; private set; }

        public CrossViewModel(string cross, IObservable<Quote> quotes)
        {
            Cross = cross;

            Ask = 3;
            Bid = 2;
            AskProvider = "MS";
            BidProvider = "JP";
            AskMovement = PriceMovement.Stale;
            BidMovement = PriceMovement.Stale;

            quotes
				.Where(q => q.Cross == Cross)
				.BufferMaxAskMinBid<Quote>(TimeSpan.FromSeconds(3))
				.Subscribe(OnQuote);
        }

        private void OnQuote(Tuple<Quote, Quote> quote)
        {
	        var bidQuote = quote.Item1;
			var askQuote = quote.Item2;

			AskMovement = Ask > askQuote.Ask
				? PriceMovement.Down
				: Ask < askQuote.Ask
						? PriceMovement.Up
						: AskMovement;

			BidMovement = Bid > bidQuote.Bid
				? PriceMovement.Down
				: Bid < askQuote.Bid
						? PriceMovement.Up
						: BidMovement;

			Ask = askQuote.Ask;
			Bid = bidQuote.Bid;
			AskProvider = askQuote.Provider;
			BidProvider = bidQuote.Provider;
        }
    }
}