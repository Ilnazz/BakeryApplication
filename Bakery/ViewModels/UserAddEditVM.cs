using Bakery.Models;
using Bakery.ViewModels.Base;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.IO;
using Bakery.DataTypes.Enums;

namespace Bakery.ViewModels
{
    public class UserAddEditVM : WorkspaceVM, INotifyDataErrorInfo
    {
        
        public bool IsAdmin
        {
            get => EditingUser.Employee.RoleId == (int)EmployeeRole.Administrator;
        }

        #region Constructor
        public UserAddEditVM(int userId = 0)
        {
            if (userId == 0)
            {
                DisplayTitle = "Регистрация нового пользователя";

                EditingUser = new User();
                EditingUser.Employee = new Employee();
                EditingUser.Employee.Gender = new Gender();
                EditingUser.Employee.Role = new Role();

                _dbContext.Users.Add(EditingUser);
            }
            else
            {
                DisplayTitle = "Редактирование данных пользователя";

                EditingUser = _dbContext.Users.First(u => u.Id == userId);

                Gender = (EmployeeGender)EditingUser.Employee.GenderId;
                Role = (EmployeeRole)EditingUser.Employee.RoleId;
                Photo = EditingUser.Photo;
            }

            SaveCommand = new RelayCommand(Save, CanSave);
            ChoosePhotoCommand = new RelayCommand(ChoosePhoto);
            RemovePhotoCommand = new RelayCommand(RemovePhoto);
        }
        #endregion

        #region Properties
        private DBEntities _dbContext = new DBEntities();

        public User EditingUser { get; set; }

        public bool IsNewUser { get => EditingUser.Id == 0; }

        private byte[] _photo = null;
        public byte[] Photo
        {
            get => _photo;
            set
            {
                Set(ref _photo, value);
                EditingUser.Photo = value;
            }
        }

        public string Surname
        {
            get => EditingUser.Employee.Surname;
            set
            {
                ValidateSurname(value);
                EditingUser.Employee.Surname = value;
            }
        }

        public string Name
        {
            get => EditingUser.Employee.Name;
            set
            {
                ValidateName(value);
                EditingUser.Employee.Name = value;
            }
        }

        public string Patronymic
        {
            get => EditingUser.Employee.Patronymic;
            set
            {
                ValidatePatronymic(value);
                EditingUser.Employee.Patronymic = value;
            }
        }

        private EmployeeGender? _gender = null;
        public EmployeeGender? Gender
        {
            get => _gender;
            set {
                Set(ref _gender, value);
                if (value != null)
                    EditingUser.Employee.GenderId = (int)value;
            }
        }

        private EmployeeRole? _role = null;
        public EmployeeRole? Role
        {
            get => _role;
            set
            {
                Set(ref _role, value);
                if (value != null)
                    EditingUser.Employee.RoleId = (int)value;
            }
        }

        public string Salary
        {
            get => EditingUser.Employee.Salary > 0 ? $"{EditingUser.Employee.Salary:f}" : "";
            set
            {
                if (ValidateSalary(value) == true)
                    EditingUser.Employee.Salary = decimal.Parse(value);
                else
                    EditingUser.Employee.Salary = 0;
            }
        }

        public string Login
        {
            get => EditingUser.Login;
            set
            {
                ValidateLogin(value);
                EditingUser.Login = value;
            }
        }

        public string Password
        {
            get => EditingUser.Password;
            set
            {
                ValidatePassword(value);
                EditingUser.Password = value;
            }
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
        private bool ValidateSurname(string value)
        {
            var isValid = true;
            _errorsVM.ClearErrors("Surname");

            if (string.IsNullOrEmpty(value) == true)
            {
                _errorsVM.AddError("Surname", "Фамилия не может быть пустой");
                isValid = false;
            }

            if (value.Length > 50)
            {
                _errorsVM.AddError("Surname", "Фамилия должна содержать не более 50 символов");
                isValid = false;
            }

            return isValid;
        }

        private bool ValidateName(string value)
        {
            var isValid = true;
            _errorsVM.ClearErrors("Name");

            if (string.IsNullOrEmpty(value) == true)
            {
                _errorsVM.AddError("Name", "Имя не может быть пустым");
                isValid = false;
            }

            if (value.Length > 50)
            {
                _errorsVM.AddError("Name", "Имя должно содержать не более 50 символов");
                isValid = false;
            }

            return isValid;
        }

        private bool ValidatePatronymic(string value)
        {
            var isValid = true;
            _errorsVM.ClearErrors("Patronymic");

            if (string.IsNullOrEmpty(value) == true)
            {
                _errorsVM.AddError("Patronymic", "Отчество не может быть пустым");
                isValid = false;
            }

            if (value.Length > 50)
            {
                _errorsVM.AddError("Patronymic", "Отчество должно содержать не более 50 символов");
                isValid = false;
            }

            return isValid;
        }

        private bool ValidateSalary(string value)
        {
            var isValid = true;
            _errorsVM.ClearErrors("Salary");

            if (string.IsNullOrEmpty(value))
            {
                _errorsVM.AddError("Salary", "Необходимо указать зарплату");
                isValid = false;
            }

            if (decimal.TryParse(value, out var price) == false)
            {
                _errorsVM.AddError("Salary", "Зарплата должна быть десятичным числом");
                isValid = false;
            }

            if (price <= 0)
            {
                _errorsVM.AddError("Salary", "Зарплата должна быть больше нуля");
                isValid = false;
            }

            return isValid;
        }

        private bool ValidateLogin(string value)
        {
            var isValid = true;
            _errorsVM.ClearErrors("Login");

            if (string.IsNullOrEmpty(value) == true)
            {
                _errorsVM.AddError("Login", "Необходимо указать логин");
                isValid = false;
            }

            if (value.Length > 50)
            {
                _errorsVM.AddError("Login", "Логин должен содержать не более 50 символов");
                isValid = false;
            }

            var isUserExist = _dbContext.Users
                .Count(u => u.Login == Login && u.Password == Password) > 1;
            if (isUserExist)
            {
                _errorsVM.AddError("Login", "Пользователь с таким логином уже существует");
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
                _errorsVM.AddError("Password", "Необходимо указать пароль");
                isValid = false;
            }

            if (value.Length > 50)
            {
                _errorsVM.AddError("Password", "Пароль должен содержать не более 50 символов");
                isValid = false;
            }

            return isValid;
        }
        #endregion

        #endregion

        #region Commands

        #region Photo choosing
        public ICommand ChoosePhotoCommand { get; }

        private void ChoosePhoto(object param)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "All image files (*.png, *.jpg, *.jpeg)|*.png;*.jpg;*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|JPEG Files (*.jpeg)|*.jpeg",
                CheckPathExists = true
            };

            if (openFileDialog.ShowDialog().GetValueOrDefault() == false)
                return;

            Photo = File.ReadAllBytes(openFileDialog.FileName);
        }
        #endregion

        #region Photo removing
        public ICommand RemovePhotoCommand { get; }

        private void RemovePhoto(object param) => Photo = null;
        #endregion

        #region Saving / registering
        public ICommand SaveCommand { get; }

        private void Save(object param)
        {
            Surname = Surname.Trim(); Name = Name.Trim(); Patronymic = Patronymic.Trim();
            Login = Login.Trim(); Password = Password.Trim();

            if (IsNewUser)
            {
                _dbContext.Employees.Add(EditingUser.Employee);
                _dbContext.Users.Add(EditingUser);
                _dbContext.SaveChanges();

                MessageBox.Show("Новый пользователь зарегистрирован");
            }
            else
                MessageBox.Show("Изменения сохранены");

            _dbContext.SaveChanges();
        }

        private bool CanSave(object param)
        {
            if (_dbContext.ChangeTracker.HasChanges() == false)
                return false;
            else if (string.IsNullOrWhiteSpace(Surname)
                || string.IsNullOrWhiteSpace(Name)
                || string.IsNullOrWhiteSpace(Patronymic)
                || string.IsNullOrWhiteSpace(Login)
                || string.IsNullOrWhiteSpace(Password)
                || Gender == null
                || Role == null)
                return false;
            else if (HasErrors)
                return false;
            return true;
        }
        #endregion

        #region Closing
        protected override bool Close()
        {
            if (_dbContext.ChangeTracker.HasChanges() == false)
                return true;

            var result = MessageBox.Show("Сохранить изменения?",
                "Подтверждение",
                MessageBoxButton.YesNoCancel,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Save(null);
                return true;
            }
            else if (result == MessageBoxResult.No)
            {
                var entry = _dbContext.ChangeTracker
                    .Entries().First(e => e.State != EntityState.Unchanged);

                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.CurrentValues.SetValues(entry.OriginalValues);
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Unchanged;
                        break;
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _dbContext.Dispose();
        }
        #endregion

        #endregion
    }
}
