using System.Windows.Input;
using System;

namespace Bakery.ViewModels.Base
{
    public class RelayCommand : ICommand
    {
        #region Constructors

        public RelayCommand(Action<object> execute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = delegate { return true; };
        }

        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException(nameof(execute));
            else if (canExecute == null)
                throw new ArgumentNullException(nameof(canExecute));

            _execute = execute;
            _canExecute = canExecute;
        }

        public RelayCommand(Action<object> execute, Func<bool> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException(nameof(execute));
            else if (canExecute == null)
                throw new ArgumentNullException(nameof(canExecute));

            _execute = execute;
            _canExecute = delegate { return canExecute(); };
        }




        public RelayCommand(Action execute)
        {
            if (execute == null)
                throw new ArgumentNullException(nameof(execute));

            _execute = delegate { execute(); };
            _canExecute = delegate { return true; };
        }

        public RelayCommand(Action execute, Predicate<object> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException(nameof(execute));
            else if (canExecute == null)
                throw new ArgumentNullException(nameof(canExecute));

            _execute = delegate { execute(); };
            _canExecute = canExecute;
        }

        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException(nameof(execute));
            else if (canExecute == null)
                throw new ArgumentNullException(nameof(canExecute));

            _execute = delegate { execute(); };
            _canExecute = delegate { return canExecute(); };
        }
        #endregion Constructors

        #region Properties
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
        #endregion

        #region Methods
        public bool CanExecute(object parameter)
            => _canExecute?.Invoke(parameter) ?? true;

        public void Execute(object parameter)
            => _execute(parameter);
        #endregion
    }
}
