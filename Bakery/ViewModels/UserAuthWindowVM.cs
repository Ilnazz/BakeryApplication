using Bakery.Models;
using Bakery.ViewModels.Base;
using System;
using System.Linq;
using System.Windows.Input;
using System.Windows.Threading;
using System.Windows;
using Bakery.Views;
using Bakery.Views.Windows;
using System.ComponentModel;
using System.Data.Entity;
using System.Collections;

namespace Bakery.ViewModels
{
    public class UserAuthWindowVM : ViewModelBase, INotifyDataErrorInfo
    {
        #region Constructor
        public UserAuthWindowVM()
        {
            AuthorizeCommand = new RelayCommand(Authorize, CanAuthorize);

            SetRememberedUserLoginAndPassword();

            InitializeTimer();
        }
        #endregion

        #region Properties
        private string _login = "";
        public string Login
        {
            get => _login;
            set {
                ValidateLogin(value);
                Set(ref _login, value);
            }
        }

        private string _password = "";
        public string Password
        {
            get => _password;
            set
            {
                ValidatePassword(value);
                Set(ref _password, value);
            }
        }

        private bool _rememberUser = true;
        public bool RememberUser
        {
            get => _rememberUser;
            set => Set(ref _rememberUser, value);
        }
        #endregion

        #region Validation

        #region Errors
        private readonly ErrorsVM _errorsVM = new ErrorsVM();

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public bool HasErrors => _errorsVM.HasErrors;

        public IEnumerable GetErrors(string propName) => _errorsVM.GetErrors(propName);
        #endregion

        #region Validators
        private bool ValidateLogin(string value)
        {
            var isValid = true;
            _errorsVM.ClearErrors("Login");

            if (string.IsNullOrEmpty(value) == true)
            {
                _errorsVM.AddError("Login", "Логин не может быть пустым");
                isValid = false;
            }

            return isValid;
        }

        private bool ValidatePassword(string value)
        {
            var isValid = true;
            _errorsVM.ClearErrors("Password");

            if (string.IsNullOrEmpty(value) == true)
            {
                _errorsVM.AddError("Password", "Пароль не может быть пустым");
                isValid = false;
            }

            return isValid;
        }
        #endregion

        #endregion

        #region Timer
        private readonly DispatcherTimer _timer = new DispatcherTimer();

        #region Timer Properties
        private const int MAX_ATTEMPTS_NUMBER = 3;
        private const int TIMER_TOTAL_SECONDS = 5;
        
        private bool _isTimerWorking = false;
        public bool IsTimerWorking
        {
            get => _isTimerWorking;
            set => Set(ref _isTimerWorking, value);
        }

        private Visibility _timerVisibility = Visibility.Collapsed;
        public Visibility TimerVisibility
        {
            get => _timerVisibility;
            set => Set(ref _timerVisibility, value);
        }

        private int _timerSeconds = 0;
        public int TimerSeconds
        {
            get => _timerSeconds;
            set => Set(ref _timerSeconds, value);
        }

        private int _authorizationAttemptNumber = 0;
        #endregion

        #region Timer methods
        private void InitializeTimer()
        {
            _timer.Tick += (s, e) => TimerTick();
            _timer.Interval = new TimeSpan(0, 0, 1);

            var dateTimesOffset = DateTime.Now - Properties.Settings.Default.LastAuthorizationAttempt;

            if (dateTimesOffset.TotalSeconds < TIMER_TOTAL_SECONDS)
                StartTimer(TIMER_TOTAL_SECONDS - (int)dateTimesOffset.TotalSeconds);
            else
            {
                Properties.Settings.Default.LastAuthorizationAttempt = DateTime.MinValue;
                Properties.Settings.Default.Save();
            }
        }

        private void TimerTick()
        {
            if (_timerSeconds > 0)
                TimerSeconds--;
            else
            {
                ResetTimer();
                _authorizationAttemptNumber = 0;
            }
        }

        private void StartTimer(int seconds)
        {
            IsTimerWorking = true;
            TimerSeconds = seconds;
            TimerVisibility = Visibility.Visible;

            _timer.Start();

            Properties.Settings.Default.LastAuthorizationAttempt = DateTime.Now;
            Properties.Settings.Default.Save();
        }

        private void ResetTimer()
        {
            IsTimerWorking = false;
            TimerVisibility = Visibility.Collapsed;

            _timer.Stop();

            Properties.Settings.Default.LastAuthorizationAttempt = DateTime.MinValue;
            Properties.Settings.Default.Save();
        }

        private void StartTimerIfMaxAttemptsNumberExceeded()
        {
            if (++_authorizationAttemptNumber == 3)
            {
                _authorizationAttemptNumber = 0;
                StartTimer(TIMER_TOTAL_SECONDS);
            }
        }
        #endregion

        #endregion

        #region Commands

        #region Authorization
        public ICommand AuthorizeCommand { get; }

        private void Authorize(object parameter)
        {
            User user;
            using (var dbContext = new DBEntities())
                user = dbContext.Users
                    .FirstOrDefault(u => u.Login == _login.Trim()
                        && u.Password == _password.Trim());

            if (user == null)
            {
                MessageBox.Show("Пользователя с такими данными не существует");

                StartTimerIfMaxAttemptsNumberExceeded();

                return;
            }

            if (_rememberUser == true)
                RememberUserLoginAndPassword();
            else
                ResetRememberedUserLoginAndPassword();

            OpenMainWindow(user);

            var currentWindow = parameter as Window;
            currentWindow.Close();
        }

        private void OpenMainWindow(User user)
        {
            MainWindow mainWindow = new MainWindow();

            var mainWindowVM = new MainWindowVM(user.Id);
            mainWindowVM.Closing += delegate { mainWindow.Close(); };

            mainWindow.Closing += (s, e) =>
            {
                if (mainWindowVM.OnClose() == false)
                    e.Cancel = true;
            };
            
            mainWindow.DataContext = mainWindowVM;
            mainWindow.Show();
        }

        private bool CanAuthorize(object param)
            => _isTimerWorking == false
                && HasErrors == false;
        #endregion

        #endregion

        #region Remembering user login and password methods
        private void SetRememberedUserLoginAndPassword()
        {
            if (Properties.Settings.Default.RememberedUserLogin != null)
                Login = Properties.Settings.Default.RememberedUserLogin;
            if (Properties.Settings.Default.RememberedUserPassword != null)
                Password = Properties.Settings.Default.RememberedUserPassword;
        }

        private void RememberUserLoginAndPassword()
        {
            Properties.Settings.Default.RememberedUserLogin = _login;
            Properties.Settings.Default.RememberedUserPassword = _password;
            Properties.Settings.Default.Save();
        }

        private void ResetRememberedUserLoginAndPassword()
        {
            Properties.Settings.Default.RememberedUserLogin = null;
            Properties.Settings.Default.RememberedUserPassword = null;
            Properties.Settings.Default.Save();
        }
        #endregion
    }
}
