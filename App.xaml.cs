using System.Windows;
using PoputkeeLite.Core.Models;
using PoputkeeLite.Core.Services;
using PoputkeeLite.Desktop.Views;

namespace PoputkeeLite
{
    public partial class App : Application
    {
        // =======================================================
        // 1. Статические свойства (публичные)
        // =======================================================

        /// <summary>
        /// Текущий аутентифицированный пользователь приложения.
        /// Null если пользователь не выполнил вход.
        /// </summary>
        public static User? CurrentUser { get; set; }

        // =======================================================
        // 2. Переопределенные методы жизненного цикла
        // =======================================================

        /// <summary>
        /// Инициализация приложения при запуске.
        /// Определяет главное окно в зависимости от сохраненной сессии.
        /// </summary>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Конфигурация завершения приложения
            ShutdownMode = ShutdownMode.OnMainWindowClose;

            // Инициализация файлов данных
            DataService.InitializeDataFiles();

            // Проверка сохраненной сессии пользователя
            var savedUser = SessionService.LoadSession();
            if (savedUser != null)
            {
                // Загрузка авторизованной сессии
                CurrentUser = savedUser;
                var mainWindow = new MainWindow();
                mainWindow.Show();
                Current.MainWindow = mainWindow;
            }
            else
            {
                // Показать окно аутентификации
                var loginWindow = new LoginView();
                loginWindow.Show();
                Application.Current.MainWindow = loginWindow;
            }
        }
    }
}