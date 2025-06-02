using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PoputkeeLite.Desktop.ViewModels;
using System.Windows;
using System.Windows.Controls;
using PoputkeeLite.ViewModels;


namespace PoputkeeLite.Desktop.Views
{
    /// <summary>
    /// Логика взаимодействия для TripView.xaml
    /// </summary>
    public partial class TripView : UserControl
    {
        public TripView()
        {
            InitializeComponent();
            DataContext = new TripViewModel();
        }
    }
}
