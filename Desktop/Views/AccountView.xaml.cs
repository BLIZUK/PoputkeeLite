using System;
using System.Windows;
using System.Windows.Controls;
using PoputkeeLite.Desktop.ViewModels;


namespace PoputkeeLite.Desktop.Views
{
    /// <summary>
    /// Логика взаимодействия для AccountView.xaml
    /// </summary>
    public partial class AccountView : UserControl
    {
        public AccountView()
        {
            InitializeComponent();
            DataContext = new AccountViewModel();
        }
    }
}
