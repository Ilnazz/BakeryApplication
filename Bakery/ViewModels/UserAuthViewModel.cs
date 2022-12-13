﻿using Bakery.Models;
using Bakery.ViewModels.Base;
using System;
using System.Linq;
using System.Windows.Input;
using System.Windows.Threading;
using System.Windows;
using Bakery.Views;

namespace Bakery.ViewModels
{
    public class UserAuthViewModel : ViewModelBase
    {
        #region Constructor
        public UserAuthViewModel()
        {
            AuthorizeCommand = new RelayCommand(Authorize, CanAuthorize);
            NavigateToRegistrationWindowCommand = new RelayCommand(NavigateToRegistrationWindow);

            SetRememberedUserLoginAndPassword();

            InitializeTimer();
        }
        #endregion

        #region Properties
        private string _login = "";
        public string Login
        {
            get => _login;
            set => Set(ref _login, value);
        }

        private string _password = "";
        public string Password
        {
            get => _password;
            set => Set(ref _password, value);
        }

        private bool _rememberUser = false;
        public bool RememberUser
        {
            get => _rememberUser;
            set => Set(ref _rememberUser, value);
        }
        
        #endregion

        #region Timer
        private readonly DispatcherTimer _timer = new DispatcherTimer();

        #region Properties
        private const int MAX_ATTEMPTS_NUMBER = 3;
        private const int TIMER_TOTAL_SECONDS = 60;
        
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

        private int _authorizationAttemptNumber = MAX_ATTEMPTS_NUMBER;

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
        #region Authorize() and CanAuthorize()
        public ICommand AuthorizeCommand { get; }

        private void Authorize(object window)
        {
            if (string.IsNullOrWhiteSpace(_login))
            {
                MessageBox.Show("Логин не может быть пустым");
                return;
            }
            else if (string.IsNullOrWhiteSpace(_password))
            {
                MessageBox.Show("Пароль не может быть пустым");
                return;
            }

            User user;
            using (var dbContext = new DBEntities())
            {
                try
                {
                    user = dbContext.Users.FirstOrDefault(u => u.Login == _login.Trim() && u.Password == _password.Trim());
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}");
                    return;
                }
            }

            if (user == null)
            {
                MessageBox.Show("Пользователь с такими данными не существует");

                StartTimerIfMaxAttemptsNumberExceeded();

                return;
            }

            App.CurrentUser = user;

            if (_rememberUser == true)
                RememberUserLoginAndPassword();
            else
                ResetRememberedUserLoginAndPassword();

            MessageBox.Show("Пользователь авторизован");

            // navigate to mainwindow with pages
            //(window as Window).Close();
        }

        private bool CanAuthorize(object parameter)
            => _login != ""
                && _password != ""
                && _isTimerWorking == false;
        #endregion

        #region NavigateToRegistrationWindow()
        public ICommand NavigateToRegistrationWindowCommand { get; }

        private void NavigateToRegistrationWindow(object parameter)
        {
            var registrationWindow = new UserRegView();
            registrationWindow.ShowDialog();
        }
        #endregion
        #endregion

        #region Remember user login and password methods
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
