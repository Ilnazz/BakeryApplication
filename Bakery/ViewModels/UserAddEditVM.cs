using Bakery.Models;
using Bakery.ViewModels.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bakery.ViewModels
{
    public class UserAddEditVM : WorkspaceVM, INotifyDataErrorInfo
    {
        #region Constructor
        public UserAddEditVM(int userId = 0)
        {
            if (userId == 0)
            {
                DisplayTitle = "Добавление нового пользователя";
                EditingUser = new User();
            }
            else
            {
                DisplayTitle = "Редактирование данных пользователя";
                EditingUser = _dbContext.Users.First(u => u.Id == userId);
            }

            Photo = EditingUser.Photo;

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

        private string _surname = null;
        public string Surname
        {
            get => _surname;
            set
            {
                ValidateSurname(value);
                EditingUser.Employee.Surname = value;
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
    }
}
