using EastLodgeInterview.Client.ViewModel.Crosses;

namespace EastLodgeInterview.Client.ViewModel.Shell
{
    public interface IShellViewModel
    {
        ICrossesViewModel Crosses { get; }
    }
}