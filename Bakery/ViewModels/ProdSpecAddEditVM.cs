using Bakery.Models;
using Bakery.ViewModels.Base;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Contexts;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace Bakery.ViewModels
{
    public class ProdSpecAddEditVM : WorkspaceVM, INotifyDataErrorInfo
    {
        #region Constructor
        public ProdSpecAddEditVM(int prodSpecId = 0)
        {
            if (prodSpecId == 0)
                DisplayTitle = "Добавление спецификации продукта";
            else
                DisplayTitle = "Редактирование спецификации продукта";

            if (prodSpecId == 0)
                _editingProdSpec = new ProductSpecification();
            else
                _editingProdSpec = _dbContext.ProductSpecifications.First(ps => ps.Id == prodSpecId);

            Photo = _editingProdSpec.Photo;

            _dbContext.ProductIngredients.Load();
            Ingredients = new ObservableCollection<ProductIngredient>(_editingProdSpec.ProductIngredients);

            _dbContext.MaterialSpecifications.Load();
            var availableMaterialSpecs = _dbContext.MaterialSpecifications.Local
                .Where(ms => Ingredients.All(i => i.MaterialSpecificationId != ms.Id));

            AvailableMaterialSpecs = new ObservableCollection<MaterialSpecification>(availableMaterialSpecs);

            _dbContext.MeasureUnits.Load();
            AvailableMeasureUnits = _dbContext.MeasureUnits.Local;

            SaveCommand = new RelayCommand(Save, CanSave);
            ChoosePhotoCommand = new RelayCommand(ChoosePhoto);
            RemovePhotoCommand = new RelayCommand(RemovePhoto);
            AddIngredientCommand = new RelayCommand(AddIngredient, CanAddIngredient);
            RemoveIngredientCommand = new RelayCommand(RemoveIngredient);
        }
        #endregion

        #region Properties
        private DBEntities _dbContext = new DBEntities();

        private ProductSpecification _editingProdSpec;
        public ProductSpecification EditingProdSpec { get => _editingProdSpec; }

        public IEnumerable<MeasureUnit> AvailableMeasureUnits { get; }
        
        public bool IsNewProdSpec { get => _editingProdSpec.Id == 0; }

        private byte[] _photo = null;
        public byte[] Photo
        {
            get => _photo;
            set
            {
                Set(ref _photo, value);
                _editingProdSpec.Photo = value;
            }
        }

        public string Title
        {
            get => _editingProdSpec.Title;
            set
            {
                ValidateTitle(value);
                _editingProdSpec.Title = value;
            }
        }

        public string Description
        {
            get => _editingProdSpec.Description;
            set
            {
                ValidateDescription(value);
                _editingProdSpec.Description = value;
            }
        }

        public string Price
        {
            get => _editingProdSpec.Price > 0 ? _editingProdSpec.Price.ToString() : "";
            set
            {
                if (ValidatePrice(value) == true)
                    _editingProdSpec.Price = decimal.Parse(value);
                else
                    _editingProdSpec.Price = 0;
            }
        }

        public string Weight
        {
            get => _editingProdSpec.Weight > 0 ? _editingProdSpec.Weight.ToString() : "";
            set
            {
                if (ValidateWeight(value) == true)
                    _editingProdSpec.Weight = int.Parse(value);
                else
                    _editingProdSpec.Weight= 0;
            }
        }

        public MeasureUnit MeasureUnit {
            get => _editingProdSpec.MeasureUnit;
            set => _editingProdSpec.MeasureUnit = value;
        }

        public ObservableCollection<ProductIngredient> Ingredients { get; }

        public ObservableCollection<MaterialSpecification> AvailableMaterialSpecs { get; }
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
                _errorsVM.AddError("Title", "Такая спецификация продукта уже существует");
                isValid = false;
            }

            if (value.Length > 100)
            {
                _errorsVM.AddError("Title", "Название должно содержать не более 100 символов");
                isValid = false;
            }

            return isValid;
        }
        
        private bool ValidateDescription(string value)
        {
            var isValid = true;
            _errorsVM.ClearErrors("Description");

            if (string.IsNullOrEmpty(value) == true)
            {
                _errorsVM.AddError("Description", "Описание не может быть пустым");
                isValid = false;
            }

            return isValid;
        }

        private bool ValidatePrice(string value)
        {
            var isValid = true;
            _errorsVM.ClearErrors("Price");

            if (string.IsNullOrEmpty(value))
            {
                _errorsVM.AddError("Price", "Цена не может быть пустой");
                isValid = false;
            }

            if (decimal.TryParse(value, out var price) == false)
            {
                _errorsVM.AddError("Price", "Цена должна быть десятичным числом");
                isValid = false;
            }

            if (price <= 0)
            {
                _errorsVM.AddError("Price", "Цена должна быть больше нуля");
                isValid = false;
            }

            return isValid;
        }

        private bool ValidateWeight(string value)
        {
            var isValid = true;
            _errorsVM.ClearErrors("Weight");
            
            if (string.IsNullOrEmpty(value))
            {
                _errorsVM.AddError("Weight", "Вес не может быть пустым");
                isValid = false;
            }

            if (int.TryParse(value, out var weight) == false)
            {
                _errorsVM.AddError("Weight", "Вес должен быть целым числом");
                isValid = false;
            }

            if (weight <= 0)
            {
                _errorsVM.AddError("Weight", "Вес должен быть больше нуля");
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

        #region Ingredients adding / removing
        public ICommand AddIngredientCommand { get; }

        private void AddIngredient(object param)
        {
            var ingredient = new ProductIngredient();
            ingredient.ProductSpecificationId = EditingProdSpec.Id;
            Ingredients.Add(ingredient);
        }

        private bool CanAddIngredient(object param)
        {
            return true;
        }

        public ICommand RemoveIngredientCommand { get; }

        private void RemoveIngredient(object param)
        {
            var ingredient = param as ProductIngredient;
            Ingredients.Remove(ingredient);
        }

        #endregion

        #region Saving
        public ICommand SaveCommand { get; }

        private void Save(object param)
        {
            _editingProdSpec.Title = _editingProdSpec.Title.Trim();
            _editingProdSpec.Description = _editingProdSpec.Description.Trim();

            if (IsNewProdSpec)
            {
                _dbContext.ProductSpecifications.Add(_editingProdSpec);
                MessageBox.Show("Новая спецификация продукта добавлена");
            }
            else
                MessageBox.Show("Изменения сохранены");
            _dbContext.SaveChanges();
        }

        private bool CanSave(object param)
        {
            //if (_dbContext.ChangeTracker.HasChanges() == false)
                //return false;
            if (_editingProdSpec.MeasureUnit == null)
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

        #endregion

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _dbContext.Dispose();
        }
    }
}