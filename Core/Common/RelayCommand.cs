using System;
using System.Windows.Input;

namespace PoputkeeLite.Core.Common
{
    /// <summary>
    /// Реализация ICommand, которая делегирует выполнение указанным методам
    /// </summary>
    public class RelayCommand : ICommand
    {
        #region Приватные поля
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;
        #endregion

        #region Конструкторы
        /// <summary>
        /// Создает новую команду
        /// </summary>
        /// <param name="execute">Метод выполнения команды</param>
        /// <param name="canExecute">
        /// Метод проверки возможности выполнения команды (опционально)
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Выбрасывается если execute равен null
        /// </exception>
        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }
        #endregion

        #region Реализация ICommand
        /// <summary>
        /// Определяет, может ли команда выполняться в текущем состоянии
        /// </summary>
        /// <param name="parameter">Параметр команды</param>
        /// <returns>
        /// True - если команда может быть выполнена, иначе False
        /// </returns>
        public bool CanExecute(object parameter)
        {
            return _canExecute?.Invoke(parameter) ?? true;
        }

        /// <summary>
        /// Выполняет логику команды
        /// </summary>
        /// <param name="parameter">Параметр команды</param>
        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        /// <summary>
        /// Событие, возникающее при изменении возможности выполнения команды
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
        #endregion
    }
}