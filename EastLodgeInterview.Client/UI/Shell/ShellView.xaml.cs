using System.Windows.Controls;
using EastLodgeInterview.Client.ViewModel.Shell;

namespace EastLodgeInterview.Client.UI.Shell
{
    /// <summary>
    /// Interaction logic for ShellView.xaml
    /// </summary>
    public partial class ShellView : UserControl
    {
        public ShellView()
        {
            InitializeComponent();
        }

        public ShellView(IShellViewModel viewModel)
            : this()
        {
            DataContext = viewModel;
        }
    }
}
