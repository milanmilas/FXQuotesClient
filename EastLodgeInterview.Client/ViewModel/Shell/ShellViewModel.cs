using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EastLodgeInterview.Client.ViewModel.Crosses;

namespace EastLodgeInterview.Client.ViewModel.Shell
{
    public class ShellViewModel : IShellViewModel
    {
        public ICrossesViewModel Crosses { get; private set; }

        public ShellViewModel(ICrossesViewModel crosses)
        {
            Crosses = crosses;
        }
    }
}
