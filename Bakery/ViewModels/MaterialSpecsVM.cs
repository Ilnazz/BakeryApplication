using Bakery.Models;
using Bakery.ViewModels.Base;
using System.Collections.Generic;
using System.Windows.Input;
using System.Windows;
using System.Data.Entity;
using System.Linq;
using System.Windows.Documents;

namespace Bakery.ViewModels
{
    public class MaterialSpecsVM : WorkspaceVM
    {
        #region Constructor
        public MaterialSpecsVM()
        {
            DisplayTitle = "Спецификации материалов";

            EditCommand = new RelayCommand(Edit);
            AddCommand = new RelayCommand(Add);

            _dbContext.MaterialSpecifications.Load();
            MaterialSpecs = _dbContext.MaterialSpecifications.Local;
        }
        #endregion

        #region Properties
        private DBEntities _dbContext = new DBEntities();

        private IEnumerable<MaterialSpecification> _materialSpecs;
        public IEnumerable<MaterialSpecification> MaterialSpecs
        {
            get => _materialSpecs;
            set => Set(ref _materialSpecs, value);
        }
        #endregion

        #region Commands

        #region Editing
        public ICommand EditCommand { get; }

        private void Edit(object param)
        {
            var materialSpec = param as MaterialSpecification;

            if (IsMaterialSpecificationAlreadyEditing(materialSpec))
            {
                MessageBox.Show("Уже редактируется", "Сообщение", MessageBoxButton.OK);
                return;
            }

            var materialSpecVM = new MaterialSpecAddEditVM(materialSpec.Id);
            WorkspacesModel.Workspaces.Add(materialSpecVM);
        }

        private bool IsMaterialSpecificationAlreadyEditing(MaterialSpecification materialSpec)
            => WorkspacesModel.Workspaces.Any(viewModel =>
                {
                    if (viewModel is MaterialSpecAddEditVM materialSpecVM == true
                        && materialSpecVM.EditingMaterialSpec.Id == materialSpec.Id)
                        return true;
                    return false;
                });

        #endregion

        #region Adding
        public ICommand AddCommand { get; }

        private void Add(object param)
        {
            var materialSpecAddEditVM = new MaterialSpecAddEditVM();

            materialSpecAddEditVM.Closing += () =>
            {
            };

            WorkspacesModel.Workspaces.Add(materialSpecAddEditVM);
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
