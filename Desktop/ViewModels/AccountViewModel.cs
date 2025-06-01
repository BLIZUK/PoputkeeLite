// ViewModels/AccountViewModel.cs
using PoputkeeLite.Core.Common;
using PoputkeeLite.Core.Services;
using PoputkeeLite.Desktop.Views;
using System.Windows;
using System.Windows.Input;

namespace PoputkeeLite.Desktop.ViewModels
{
    public class AccountViewModel
    {
        public ICommand LogoutCommand { get; }

        public AccountViewModel()
        {
            LogoutCommand = new RelayCommand(_ => Logout());
        }

        private void Logout()
        {
            // Очистка сессии
            SessionService.ClearSession();
            App.CurrentUser = null;

            // Переход на окно входа
            new LoginView().Show();

            // Закрытие главного окна
            Application.Current.MainWindow?.Close();
            Application.Current.MainWindow = new LoginView();
        }
    }
}