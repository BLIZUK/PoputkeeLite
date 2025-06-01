using System.Windows;
using PoputkeeLite.Core.Models;
using PoputkeeLite.Core.Services;
using PoputkeeLite.Desktop.Views;

namespace PoputkeeLite
{
    public partial class App : Application
    {
        public static User CurrentUser { get; set; }

        // Глобальные сервисы
        //public static AuthService AuthService { get; } = new AuthService();
        //public static TripService TripService { get; } = new TripService();

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            DataService.InitializeDataFiles();

            // Проверка сохраненной сессии
            var savedUser = SessionService.LoadSession();
            if (savedUser != null)
            {
                CurrentUser = savedUser;
                new MainWindow().Show();
            }
            else
            {
                new LoginView().Show();
            }
        }
    }
}