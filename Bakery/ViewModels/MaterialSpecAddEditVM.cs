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
using System.Windows.Input;
using System.Windows;

namespace Bakery.ViewModels
{
    public class MaterialSpecAddEditVM : WorkspaceVM, INotifyDataErrorInfo
    {
        #region Constructor
        public MaterialSpecAddEditVM(int materialSpecId = 0)
        {
            if (materialSpecId == 0)
                DisplayTitle = "Добавление спецификации материала";
            else
                DisplayTitle = "Редактирование спецификации материала";

            if (materialSpecId != 0)
                _editingMaterialSpec = _dbContext.MaterialSpecifications
                                            .First(ps => ps.Id == materialSpecId);
            else
                _editingMaterialSpec = new MaterialSpecification();

            _dbContext.MeasureUnits.Load();
            AvailableMeasureUnits = _dbContext.MeasureUnits.Local;

            SaveCommand = new RelayCommand(Save, CanSave);
        }
        #endregion

        #region Properties
        private DBEntities _dbContext = new DBEntities();

        private MaterialSpecification _editingMaterialSpec;
        public MaterialSpecification EditingMaterialSpec { get => _editingMaterialSpec; }

        public IEnumerable<MeasureUnit> AvailableMeasureUnits { get; }

        public bool IsNewMaterialSpec { get => _editingMaterialSpec.Id == 0; }

        public string Title
        {
            get => _editingMaterialSpec.Title;
            set
            {
                ValidateTitle(value);
                _editingMaterialSpec.Title = value;
            }
        }

        public MeasureUnit MeasureUnit
        {
            get => _editingMaterialSpec.MeasureUnit;
            set => _editingMaterialSpec.MeasureUnit = value;
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
        private bool ValidateTitle(string value)
        {
            var isValid = true;
            _errorsVM.ClearErrors("Title");

            if (string.IsNullOrEmpty(value) == true)
            {
                _errorsVM.AddError("Title", "Название не может быть пустым");
                isValid = false;
            }

            var isAlreadyExists = _dbContext.ProductSpecifications.Count(ps => ps.Title == value) > 1;
            if (isAlreadyExists)
            {
                _errorsVM.AddError("Title", "Такая спецификация материала уже существует");
                isValid = false;
            }

            if (value.Length > 100)
            {
                _errorsVM.AddError("Title", "Название должно содержать не более 100 символов");
                isValid = false;
            }

            return isValid;
        }
        #endregion

        #endregion

        #region Commands

        #region Saving
        public ICommand SaveCommand { get; }

        private void Save(object param)
        {
            _editingMaterialSpec.Title = _editingMaterialSpec.Title.Trim();

            if (IsNewMaterialSpec)
            {
                _dbContext.MaterialSpecifications.Add(_editingMaterialSpec);
                MessageBox.Show("Новая спецификация материала добавлена");
            }
            else
                MessageBox.Show("Изменения сохранены");
            _dbContext.SaveChanges();
        }

        private bool CanSave(object param)
        {
            if (_dbContext.ChangeTracker.HasChanges() == false)
                return false;
            if (_editingMaterialSpec.MeasureUnit == null)
                return false;
            return HasErrors == false;
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
        #endregion

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _dbContext.Dispose();
        }

        #endregion
    }
}
