using Bakery.ViewModels.Base;
using System.Windows.Input;
using System.Windows;
using Bakery.DataTypes.Enums;
using Bakery.Models;
using System.Linq;
using System;

namespace Bakery.ViewModels
{
    public class UserRegViewModel : ViewModelBase
    {
        #region Constructor
        public UserRegViewModel()
        {
            RegisterCommand = new RelayCommand(Register, CanRegister);
        }
        #endregion

        #region Properties
        private string _surname = "";
        public string Surname
        {
            get => _surname;
            set => Set(ref _surname, value);
        }
        
        private string _name = "";
        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }

        private string _patronymic = "";
        public string Patronymic
        {
            get => _patronymic;
            set => Set(ref _patronymic, value);
        }

        private EmployeeGender? _gender = null;
        public EmployeeGender? Gender
        {
            get => _gender;
            set => Set(ref _gender, value);
        }

        private EmployeeRole? _role = null;
        public EmployeeRole? Role
        {
            get => _role;
            set => Set(ref _role, value);
        }

        private decimal _salary = 0;
        public decimal Salary
        {
            get => _salary;
            set => Set(ref _salary, value);
        }

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

        private string _passwordConfirmation = "";
        public string PasswordConfirmation
        {
            get => _passwordConfirmation;
            set => Set(ref _passwordConfirmation, value);
        }
        #endregion

        /*
        #region Error text
        private Visibility _errorTextVisibility = Visibility.Collapsed;
        public Visibility ErrorTextVisibility
        {
            get => _errorTextVisibility;
            set => Set(ref _errorTextVisibility, value);
        }

        private string _errorText = "";
        public string ErrorText
        {
            get => _errorText;
            set => Set(ref _errorText, value);
        }

        private void ShowError(string text)
        {

        }
        #endregion
        */

        #region Commands
        public ICommand RegisterCommand { get; }

        private void Register(object parameter)
        {
            NormalizeFileds();

            if (_password != _passwordConfirmation)
            {
                MessageBox.Show("Пароли должны совпадать");
                return;
            }

            bool isUserExist;
            using (var dbContext = new DBEntities())
            {
                try
                {
                    isUserExist = dbContext.Users.Any(u => u.Login == _login && u.Password == _password);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}");
                    return;
                }
            }

            if (isUserExist)
            {
                //ShowError("Пользователь с такими данными не существует");
                MessageBox.Show("Пользователь с такими данными уже существует");
                return;
            }

            var newEmployee = new Employee()
            {
                Surname = _surname,
                Name = _name,
                Patronymic = _patronymic,
                GenderId = (int)Gender,
                RoleId = (int)Role,
                Salary = _salary,
            };

            var newUser = new User()
            {
                Employee = newEmployee,
                Login = _login,
                Password = _password
            };

            using (var dbContext = new DBEntities())
            {
                try
                {
                    dbContext.Employees.Add(newEmployee);
                    dbContext.Users.Add(newUser);
                    dbContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}");
                    return;
                }
            }

            MessageBox.Show("Пользователь зарегистрирован");

            var currentWindow = parameter as Window;
            currentWindow.Close();
        }

        private bool CanRegister(object parameter) => AreAllFieldsFilledIn() == true;

        private bool AreAllFieldsFilledIn()
            => _surname != ""
                && _name != ""
                && _patronymic != ""
                && _gender.HasValue == true
                && _salary != 0
                && _login != ""
                && _password != ""
                && _passwordConfirmation != "";

        private void NormalizeFileds()
        {
            _surname = _surname.Trim();
            _name = _name.Trim();
            _patronymic = _patronymic.Trim();
            _login = _login.Trim();
            _password = _password.Trim();
            _passwordConfirmation = _passwordConfirmation.Trim();
        }
        #endregion
    }
}
