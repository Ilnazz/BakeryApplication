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
    public class ProdSpecsVM : WorkspaceVM
    {
        #region Constructor
        public ProdSpecsVM()
        {
            DisplayTitle = "Спецификации продуктов";

            EditCommand = new RelayCommand(Edit);
            AddCommand = new RelayCommand(Add);

            _dbContext.ProductSpecifications.Load();
            ProdSpecs = _dbContext.ProductSpecifications.Local;
        }
        #endregion

        #region Properties
        private DBEntities _dbContext = new DBEntities();

        private IEnumerable<ProductSpecification> _prodSpecs;
        public IEnumerable<ProductSpecification> ProdSpecs
        {
            get => _prodSpecs;
            set => Set(ref _prodSpecs, value);
        }
        #endregion

        #region Commands

        #region Editing
        public ICommand EditCommand { get; }

        private void Edit(object param)
        {
            var prodSpec= param as ProductSpecification;

            if (IsProductSpecificationAlreadyEditing(prodSpec))
            {
                MessageBox.Show("Продукт уже редактируется", "Сообщение", MessageBoxButton.OK);
                return;
            }

            var prodSpecVM = new ProdSpecAddEditVM(prodSpec.Id);
            WorkspacesModel.Workspaces.Add(prodSpecVM);
        }

        private bool IsProductSpecificationAlreadyEditing(ProductSpecification prodSpec)
            => WorkspacesModel.Workspaces.Any(viewModel =>
                {
                    if (viewModel is ProdSpecAddEditVM prodSpecVM == true
                        && prodSpecVM.EditingProdSpec.Id == prodSpec.Id)
                        return true;
                    return false;
                });

        #endregion

        #region Adding
        public ICommand AddCommand { get; }

        private void Add(object param)
        {
            var prodSpecVM = new ProdSpecAddEditVM();

            prodSpecVM.Closing += () =>
            {
                // refresh prodSpecs
            };

            WorkspacesModel.Workspaces.Add(prodSpecVM);
        }
        #endregion

        #region Closing

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _dbContext.Dispose();
        }
        #endregion

        #endregion
    }
}
