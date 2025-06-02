using PoputkeeLite.Core.Common;
using PoputkeeLite.Core.Services;
using PoputkeeLite.Desktop.Views;
using System.Windows;
using System.Windows.Input;

namespace PoputkeeLite.Desktop.ViewModels
{
    public class AccountViewModel : BaseViewModel
    {
        #region Приватные поля
        private string _name;
        private string _email;
        private string _phone;
        #endregion

        #region Публичные свойства
        /// <summary>
        /// Логин текущего пользователя
        /// </summary>
        public string Login => App.CurrentUser?.Login ?? "Неизвестно";

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// Email пользователя
        /// </summary>
        public string Email
        {
            get => _email;
            set { _email = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// Телефон пользователя
        /// </summary>
        public string Phone
        {
            get => _phone;
            set { _phone = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// Количество созданных поездок
        /// </summary>
        public int CreatedTripsCount { get; private set; }

        /// <summary>
        /// Количество активных бронирований
        /// </summary>
        public int ActiveBookingsCount { get; private set; }

        /// <summary>
        /// Количество завершенных бронирований
        /// </summary>
        public int CompletedBookingsCount { get; private set; }

        /// <summary>
        /// Флаг наличия ошибок валидации
        /// </summary>
        public bool HasErrors => !ValidateEmail(Email) || !ValidatePhone(Phone);

        /// <summary>
        /// Сообщение об ошибках валидации
        /// </summary>
        public string ErrorMessage
        {
            get
            {
                if (!ValidateEmail(Email)) return "Укажите корректный email";
                if (!ValidatePhone(Phone)) return "Укажите корректный телефон";
                return null;
            }
        }
        #endregion

        #region Команды
        /// <summary>
        /// Команда сохранения данных пользователя
        /// </summary>
        public ICommand SaveCommand { get; }

        /// <summary>
        /// Команда выхода из системы
        /// </summary>
        public ICommand LogoutCommand { get; }
        #endregion

        #region Конструктор
        public AccountViewModel()
        {
            // Инициализация данных
            LoadUserData();
            LoadStatistics();

            // Инициализация команд
            SaveCommand = new RelayCommand(_ => SaveUserData());
            LogoutCommand = new RelayCommand(_ => Logout());
        }
        #endregion

        #region Методы загрузки данных
        /// <summary>
        /// Загрузка данных пользователя
        /// </summary>
        private void LoadUserData()
        {
            var user = App.CurrentUser;
            if (user != null)
            {
                Name = user.Name;
                Email = user.Email;
                Phone = user.Phone;
            }
        }

        /// <summary>
        /// Загрузка статистики пользователя
        /// </summary>
        private void LoadStatistics()
        {
            var tripService = new TripService();
            var bookingService = new BookingService();

            // Статистика созданных поездок
            CreatedTripsCount = tripService.GetAllTrips()
                .Count(t => t.DriverLogin == App.CurrentUser.Login);

            // Статистика бронирований
            var bookings = bookingService.GetUserBookings(App.CurrentUser.Login);
            ActiveBookingsCount = bookings.Count(b => b.Status == "Active");
            CompletedBookingsCount = bookings.Count(b => b.Status == "Completed");

            // Уведомление об изменении свойств
            OnPropertyChanged(nameof(CreatedTripsCount));
            OnPropertyChanged(nameof(ActiveBookingsCount));
            OnPropertyChanged(nameof(CompletedBookingsCount));
        }
        #endregion

        #region Методы работы с пользователем
        /// <summary>
        /// Сохранение данных пользователя
        /// </summary>
        private void SaveUserData()
        {
            if (HasErrors)
            {
                ShowError(ErrorMessage);
                return;
            }

            // Обновление данных пользователя
            App.CurrentUser.Name = Name;
            App.CurrentUser.Email = Email;
            App.CurrentUser.Phone = Phone;

            // Сохранение в файл
            UpdateUserInFile();

            MessageBox.Show("Данные сохранены", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /// <summary>
        /// Обновление данных пользователя в файле
        /// </summary>
        private void UpdateUserInFile()
        {
            var lines = DataService.ReadAllLines(DataService.UsersFilePath);
            var newLines = new List<string>();

            foreach (var line in lines)
            {
                var parts = line.Split('|');
                if (parts[0] == App.CurrentUser.Login)
                {
                    newLines.Add($"{App.CurrentUser.Login}|{Name}|{Email}|{Phone}");
                }
                else
                {
                    newLines.Add(line);
                }
            }

            DataService.WriteAllLines(DataService.UsersFilePath, newLines);
            SessionService.SaveSession(App.CurrentUser);
        }

        /// <summary>
        /// Выход из системы
        /// </summary>
        private void Logout()
        {
            SessionService.ClearSession();
            App.CurrentUser = null;

            new LoginView().Show();
            Application.Current.MainWindow?.Close();
            Application.Current.MainWindow = new LoginView();
        }
        #endregion

        #region Вспомогательные методы
        /// <summary>
        /// Отображение ошибки валидации
        /// </summary>
        private void ShowError(string message)
        {
            MessageBox.Show(message, "Ошибка валидации", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        #endregion
    }
}