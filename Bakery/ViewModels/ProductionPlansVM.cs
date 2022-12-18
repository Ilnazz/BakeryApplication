using Bakery.Models;
using Bakery.ViewModels.Base;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Windows.Input;

namespace Bakery.ViewModels
{
    public class ProductionPlansVM : WorkspaceVM
    {
        public ProductionPlansVM()
        {
            DisplayTitle = "Планы производства продукции";

            EditCommand = new RelayCommand(Edit);
            AddCommand = new RelayCommand(Add);
            RefreshCommand = new RelayCommand(Refresh);

            _dbContext.ProductionPlans.Load();
            ProductionPlans = _dbContext.ProductionPlans.Local;
        }

        #region Properties
        private DBEntities _dbContext = new DBEntities();

        private IEnumerable<ProductionPlan> _productionPlans;
        public IEnumerable<ProductionPlan> ProductionPlans
        {
            get => _productionPlans;
            set => Set(ref _productionPlans, value);
        }
        #endregion

        #region Commands

        #region Editing
        public ICommand EditCommand { get; }

        private void Edit(object param)
        {

        }
        #endregion

        #region Adding
        public ICommand AddCommand { get; }

        private void Add(object param)
        {

        }
        #endregion

        #region Refreshing
        public ICommand RefreshCommand { get; }

        private void Refresh(object param)
        {
            _dbContext.Dispose();
            _dbContext = new DBEntities();
            _dbContext.ProductionPlans.Load();
            ProductionPlans = _dbContext.ProductionPlans.Local;
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
