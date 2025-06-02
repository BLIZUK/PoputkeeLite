// Common/BaseViewModel.cs
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace PoputkeeLite.Core.Common
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        // Вспомогательные методы валидации
        protected bool ValidateEmail(string email)
        {
            return !string.IsNullOrWhiteSpace(email) &&
                   Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        protected bool ValidatePhone(string phone)
        {
            return !string.IsNullOrWhiteSpace(phone) &&
                   Regex.IsMatch(phone, @"^\+?[0-9\s\-\(\)]{7,20}$");
        }

        protected bool ValidateRequired(string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }

        protected bool ValidatePositiveNumber(int value)
        {
            return value > 0;
        }
    }
}