using Bakery.Models;
using Bakery.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Bakery.ViewModels
{
    public class ProdSpecsVM : WorkspaceViewModel
    {
        #region Constructor
        public ProdSpecsVM()
        {
            DisplayTitle = "Спецификации продуктов";

            EditCommand = new RelayCommand(Edit);

            _dbContext.ProductSpecifications.Load();
            _productSpecifications = _dbContext.ProductSpecifications.Local;
        }
        #endregion

        #region Properties
        private DBEntities _dbContext = new DBEntities();

        private IEnumerable<ProductSpecification> _productSpecifications;
        public IEnumerable<ProductSpecification> ProductSpecifications
        {
            get => _productSpecifications;
            set => Set(ref _productSpecifications, value);
        }
        #endregion

        #region Commands

        #region Editing
        public ICommand EditCommand { get; }

        private void Edit(object parameter)
        {
            var prodSpec= parameter as ProductSpecification;

            if (IsProductSpecificationAlreadyEditing(prodSpec))
            {
                MessageBox.Show("Продукт уже редактируется", "Сообщение", MessageBoxButton.OK);
                return;
            }

            var prodSpecVM = new ProdSpecsAddEditViewModel(prodSpec);

            WorkspacesModel.Workspaces.Add(prodSpecVM);
        }

        private bool IsProductSpecificationAlreadyEditing(ProductSpecification prodSpec)
            => WorkspacesModel.Workspaces.Any(viewModel =>
                {
                    if (viewModel is ProdSpecsAddEditViewModel prodSpecVM == true
                        && prodSpecVM.EditingProductSpecification.Id == prodSpec.Id)
                        return true;
                    return false;
                });

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
