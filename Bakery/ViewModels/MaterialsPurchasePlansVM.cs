using Bakery.Models;
using Bakery.ViewModels.Base;
using System.Data.Entity;
using System.Windows.Input;
using System.Windows;
using System.Linq;
using System.Collections.Generic;

namespace Bakery.ViewModels
{
    public class MaterialsPurchasePlansVM : WorkspaceVM
    {
        private int _currentUserId;

        #region Constructor
        public MaterialsPurchasePlansVM(int userId)
        {
            DisplayTitle = "Планы закупок материалов";

            EditCommand = new RelayCommand(Edit);
            AddCommand = new RelayCommand(Add);
            RefreshCommand = new RelayCommand(Refresh);

            _currentUserId = userId;
        }
        #endregion

        #region Properties
        private DBEntities _dbContext = new DBEntities();

        private IEnumerable<MaterialsPurchasePlan> _materialsPurchasePlans;
        public IEnumerable<MaterialsPurchasePlan> MaterialsPurchasePlans
        {
            get => _materialsPurchasePlans;
            set => Set(ref _materialsPurchasePlans, value);
        }
        #endregion

        #region Commands

        #region Editing
        public ICommand EditCommand { get; }

        private void Edit(object param)
        {
            //var materialsPurchasePlan = param as MaterialsPurchasePlan;

            //if (IsMaterialPurchasePlanAlreadyEditing(materialsPurchasePlan))
            //{
            //    MessageBox.Show("Уже редактируется", "Сообщение", MessageBoxButton.OK);
            //    return;
            //}

            //var prodSpecVM = new MaterialsPurchasePlanAddEditVM(materialPurchasePlan.Id);
            //WorkspacesModel.Workspaces.Add(prodSpecVM);
        }

        //private bool IsMaterialPurchasePlanAlreadyEditing(MaterialsPurchasePlan materialsPurchasePlan)
        //    => WorkspacesModel.Workspaces.Any(viewModel =>
        //    {
        //        if (viewModel is ProdSpecAddEditVM materialsPurchasePlanVM == true
        //            && materialsPurchasePlanVM.EditingMaterialsPurchasePlan.Id == materialsPurchasePlan.Id)
        //            return true;
        //        return false;
        //    });

        #endregion

        #region Adding
        public ICommand AddCommand { get; }

        private void Add(object param)
        {
            //var materialsPurchasePlanVM = new MaterialsPurchasePlanAddEditVM();

            //WorkspacesModel.Workspaces.Add(materialsPurchasePlanVM);
        }
        #endregion

        #region Refreshing
        public ICommand RefreshCommand { get; }

        private void Refresh(object param)
        {
            _dbContext.Dispose();
            _dbContext = new DBEntities();
            LoadMaterialsPurchasePlansAccordingToUser();
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

        private void LoadMaterialsPurchasePlansAccordingToUser()
        {
            _dbContext.MaterialsPurchasePlans.Load();
            MaterialsPurchasePlans = _dbContext.MaterialsPurchasePlans.Local
                .Where(mpp => mpp.Employees.Any(emp => emp.Users.First().Id == _currentUserId));
        }
    }
}
