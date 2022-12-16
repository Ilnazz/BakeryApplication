using Bakery.Models;
using Bakery.ViewModels.Base;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace Bakery.ViewModels
{
    public class ProdSpecsAddEditViewModel : WorkspaceViewModel, INotifyDataErrorInfo
    {
        #region Constructor
        public ProdSpecsAddEditViewModel(ProductSpecification prodSpec = null)
        {
            if (prodSpec == null)
                DisplayTitle = "Добавление спецификации продукта";
            else
                DisplayTitle = "Редактирование спецификации продукта";

            _dbContext.MeasureUnits.Load();
            AvailableMeasureUnits = _dbContext.MeasureUnits.Local;

            _editingProductSpecification = prodSpec ?? new ProductSpecification();

            SaveCommand = new RelayCommand(Save, CanSave);
            ChoosePhotoCommand = new RelayCommand(ChoosePhoto);
        }
        #endregion

        #region Properties
        private DBEntities _dbContext = new DBEntities();

        private ProductSpecification _editingProductSpecification;
        public ProductSpecification EditingProductSpecification { get => _editingProductSpecification; }

        private byte[] _editingProductSpecificationPhoto = null;
        public byte[] EditingProductSpecificationPhoto
        {
            get => _editingProductSpecificationPhoto;
            set
            {
                Set(ref _editingProductSpecificationPhoto, value);
                _editingProductSpecification.Photo = value;
            }
        }

        public string Title
        {
            get => _editingProductSpecification.Title;
            set
            {
                if (IsTitleValid(value))
                    _editingProductSpecification.Title = value;
            }
        }

        public bool IsTitleValid(string value)
        {
            var isValid = true;

            if (string.IsNullOrEmpty(value) == true)
            {
                AddError("Title", "Название не может быть пустым", false);
                isValid = false;
            }
            else
                RemoveError("Title", "Название не может быть пустым");

            if (Regex.IsMatch(value, @"$[а-яА-Я]+^") == false)
            {
                AddError("Title", "Название может содержать только русские буквы и пробелы", false);
                isValid = false;
            }
            else
                RemoveError("Title", "Название может содержать только русские буквы и пробелы");

            return isValid;
        }

        private Dictionary<String, List<String>> _errors =
            new Dictionary<string, List<string>>();

        // Adds the specified error to the errors collection if it is not 
        // already present, inserting it in the first position if isWarning is 
        // false. Raises the ErrorsChanged event if the collection changes. 
        public void AddError(string propertyName, string error, bool isWarning)
        {
            if (!_errors.ContainsKey(propertyName))
                _errors[propertyName] = new List<string>();

            if (!_errors[propertyName].Contains(error))
            {
                if (isWarning) _errors[propertyName].Add(error);
                else _errors[propertyName].Insert(0, error);
                RaiseErrorsChanged(propertyName);
            }
        }

        // Removes the specified error from the errors collection if it is
        // present. Raises the ErrorsChanged event if the collection changes.
        public void RemoveError(string propertyName, string error)
        {
            if (_errors.ContainsKey(propertyName) &&
                _errors[propertyName].Contains(error))
                {
                    _errors[propertyName].Remove(error);
                    if (_errors[propertyName].Count == 0) _errors.Remove(propertyName);
                    RaiseErrorsChanged(propertyName);
                }
        }

        public void RaiseErrorsChanged(string propertyName)
            => ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));

        #region INotifyDataErrorInfo Members

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName) ||
                !_errors.ContainsKey(propertyName)) return null;
            return _errors[propertyName];
        }

        public bool HasErrors
        {
            get { return _errors.Count > 0; }
        }

        #endregion

        public IEnumerable<MeasureUnit> AvailableMeasureUnits { get; }
        #endregion

        #region Commands

        #region Photo choosing
        public ICommand ChoosePhotoCommand { get; }

        private void ChoosePhoto()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "All image files (*.png, *.jpg, *.jpeg)|*.png;*.jpg;*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|JPEG Files (*.jpeg)|*.jpeg",
                CheckPathExists = true
            };

            if (openFileDialog.ShowDialog().GetValueOrDefault() == false)
                return;

            EditingProductSpecificationPhoto = File.ReadAllBytes(openFileDialog.FileName);
        }
        #endregion

        #region Saving
        public ICommand SaveCommand { get; }

        private void Save(object parameter)
        {

        }

        private bool CanSave(object parameter)
        {
            return true;
        }
        #endregion

        #region Closing
        protected override void Close(object parameter)
        {
            _dbContext.Dispose();
        }

        #endregion

        #endregion
    }
}
