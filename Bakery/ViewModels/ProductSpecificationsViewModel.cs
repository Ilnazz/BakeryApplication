using Bakery.Models;
using Bakery.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Bakery.ViewModels
{
    public class ProductSpecificationsViewModel : WorkspaceViewModel
    {
        #region Constructor
        public ProductSpecificationsViewModel()
        {
            DisplayTitle = "Ассортимент продуктов";

            using (var dbContext = new DBEntities())
            {
                try
                {
                    _productSpecifications = dbContext.ProductSpecifications.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при работе с базой данных: {ex.Message}");
                }
            }
        }
        #endregion

        #region Properties
        private IEnumerable<ProductSpecification> _productSpecifications;
        public IEnumerable<ProductSpecification> ProductSpecifications
        {
            get => _productSpecifications;
            set => Set(ref _productSpecifications, ProductSpecifications);
        }
        #endregion

        #region Commands

        #region Closing
        protected override void Close(object parameter)
        {

        }

        protected override bool CanClose(object parameter)
        {
            return true;
        }
        #endregion

        #endregion

        #region Help methods
        private void LoadProductSpecifications()
        {

        }
        #endregion
    }
}
