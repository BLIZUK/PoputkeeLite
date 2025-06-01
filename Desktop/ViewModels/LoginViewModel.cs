// ViewModels/LoginViewModel.cs
using PoputkeeLite.Core.Common;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using PoputkeeLite.Desktop.Views;
using PoputkeeLite.Core.Models;
using PoputkeeLite.Core.Services;


namespace PoputkeeLite.Desktop.ViewModels
{
    public class LoginViewModel
    {
        public string Login { get; set; }
        public ICommand LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(_ => Authenticate());
        }

        private void Authenticate()
        {
            if (string.IsNullOrWhiteSpace(Login)) return;

            // Чтение/создание пользователя
            var user = GetOrCreateUser(Login);

            // Сохранение сессии
            SessionService.SaveSession(user);
            App.CurrentUser = user;

            // Переход в главное окно
            new MainWindow().Show();
            CloseCurrentWindow();
        }

        private User GetOrCreateUser(string login)
        {
            // Чтение существующих пользователей
            var users = new List<User>();
            var lines = DataService.ReadAllLines(DataService.UsersFilePath);

            foreach (var line in lines)
            {
                var parts = line.Split('|');
                users.Add(new User
                {
                    Login = parts[0],
                    Name = parts.Length > 1 ? parts[1] : "",
                    Email = parts.Length > 2 ? parts[2] : "",
                    Phone = parts.Length > 3 ? parts[3] : ""
                });
            }

            // Поиск существующего пользователя
            var user = users.FirstOrDefault(u => u.Login == login);

            // Создание нового пользователя если не существует
            if (user == null)
            {
                user = new User { Login = login };
                DataService.AppendLine(DataService.UsersFilePath, $"{login}|||");
            }

            return user;
        }

        private static void CloseCurrentWindow()
        {
            var window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
            window?.Close();
        }
    }
}