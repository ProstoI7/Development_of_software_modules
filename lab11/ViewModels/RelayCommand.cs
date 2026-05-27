using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace lab9.ViewModels
{
    /// <summary>
    /// Этот класс реализует интерфейс ICommand и позволяет делегировать
    /// выполнение команды методу ViewModel, а также определять доступность команды через предикат
    /// CanExecute:
    /// </summary>
    // Для команд без параметра:
    public class RelayCommand(Action execute, Func<bool>? canExecute = null) : ICommand
    {
        private readonly Action _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        private readonly Func<bool>? _canExecute = canExecute;

        public bool CanExecute(object? parameter = null) => _canExecute?.Invoke() ?? true;

        public void Execute(object? parameter) => _execute.Invoke();

        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }

    // Для команд с параметром:
    public class RelayCommand<T>(Action<T?> execute, Predicate<T?>? canExecute = null) : ICommand
    {
        private readonly Action<T?> _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        private readonly Predicate<T?>? _canExecute = canExecute;

        public bool CanExecute(object? parameter) => _canExecute?.Invoke((T?)parameter) ?? true;

        public void Execute(object? parameter) => _execute.Invoke((T?)parameter);

        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
