namespace EastLodgeInterview.Client.ViewModel.Crosses
{
    public enum PriceMovement
    {
        Up,
        Down,
        Stale
    }
    public interface ICrossViewModel
    {
        string Cross { get; }
        decimal Ask { get; }
        decimal Bid { get; }
        decimal Spread { get; }
        string AskProvider { get; }
        string BidProvider { get; }
        PriceMovement AskMovement { get; }
        PriceMovement BidMovement { get; }
    }
}