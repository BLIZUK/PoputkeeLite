// ViewModels/AccountViewModel.cs
using PoputkeeLite.Core.Common;
using PoputkeeLite.Core.Services;
using PoputkeeLite.Desktop.Views;
using System.Windows;
using System.Windows.Input;

namespace PoputkeeLite.Desktop.ViewModels
{
    public class AccountViewModel : BaseViewModel
    {

        public bool HasErrors => !ValidateEmail(Email) || !ValidatePhone(Phone);

        private string _name;
        private string _email;
        private string _phone;

        public int CreatedTripsCount { get; private set; }
        public int ActiveBookingsCount { get; private set; }
        public int CompletedBookingsCount { get; private set; }

        public string Login => App.CurrentUser?.Login ?? "Неизвестно";

        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); }
        }

        public string Email
        {
            get => _email;
            set { _email = value; OnPropertyChanged(); }
        }

        public string Phone
        {
            get => _phone;
            set { _phone = value; OnPropertyChanged(); }
        }

        public ICommand SaveCommand { get; }
        public ICommand LogoutCommand { get; }

        public AccountViewModel()
        {
            // Загружаем данные пользователя
            LoadUserData();
            // Загужаем статистику
            LoadStatistics();

            SaveCommand = new RelayCommand(_ => SaveUserData());
            LogoutCommand = new RelayCommand(_ => Logout());
        }

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

            OnPropertyChanged(nameof(CreatedTripsCount));
            OnPropertyChanged(nameof(ActiveBookingsCount));
            OnPropertyChanged(nameof(CompletedBookingsCount));
        }

        public string ErrorMessage
        {
            get
            {
                if (!ValidateEmail(Email)) return "Укажите корректный email";
                if (!ValidatePhone(Phone)) return "Укажите корректный телефон";
                return null;
            }
        }

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
        private void ShowError(string message)
        {
            MessageBox.Show(message, "Ошибка валидации", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void SaveUserData()
        {
            if (HasErrors)
            {
                ShowError(ErrorMessage);
                return;
            }

            // Обновляем данные пользователя
            App.CurrentUser.Name = Name;
            App.CurrentUser.Email = Email;
            App.CurrentUser.Phone = Phone;

            // Сохраняем в файл
            UpdateUserInFile();

            MessageBox.Show("Данные сохранены", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
        }

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

        private void Logout()
        {
            SessionService.ClearSession();
            App.CurrentUser = null;

            new LoginView().Show();
            Application.Current.MainWindow?.Close();
            Application.Current.MainWindow = new LoginView();
        }
    }
}