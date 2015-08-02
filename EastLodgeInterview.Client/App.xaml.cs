using System.Windows;
using Domain;
using EastLodgeInterview.Client.UI.Shell;
using EastLodgeInterview.Client.ViewModel.Crosses;
using EastLodgeInterview.Client.ViewModel.Shell;
using log4net;

namespace EastLodgeInterview.Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(App));

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            InitializeLogging();
            Start();
        }

        private void Start()
        {
            MainWindow = new MainWindow();

	        var crosses = ConfigurationService.Crosses;
	        var providers = ConfigurationService.Providers;

            ICrossesViewModel csvm = new CrossesViewModel();
            IShellViewModel svm = new ShellViewModel(csvm);
            MainWindow.Content = new ShellView(svm);
            MainWindow.Show();
        }

        private void InitializeLogging()
        {
            log4net.Config.XmlConfigurator.Configure();
            Log.Info(@"FX Trader Starting....");
        }
    }
}
