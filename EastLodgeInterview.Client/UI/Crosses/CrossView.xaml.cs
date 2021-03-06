﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using EastLodgeInterview.Client.ViewModel.Crosses;

namespace EastLodgeInterview.Client.UI.Crosses
{
    /// <summary>
    /// Interaction logic for CrossView.xaml
    /// </summary>
    public partial class CrossView : UserControl
    {
        public CrossView()
        {
            InitializeComponent();
        }

        public CrossView(ICrossViewModel viewModel)
            : this()
        {
            DataContext = viewModel;
        }
    }
}
