using System.Collections.ObjectModel;

namespace EastLodgeInterview.Client.ViewModel.Crosses
{
    public interface ICrossesViewModel
    {
        ObservableCollection<ICrossViewModel> Crosses { get; }
    }
}