using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bakery.Models
{
    public partial class MaterialsPurchasePlan
    {
        public decimal TotalPrice {
            get => MaterialsPurchasePlan_SupplierAndMaterialSpecification
                .Sum(materialSpecAndSupplierAndQuantity =>
                    materialSpecAndSupplierAndQuantity.Quantity
                        * materialSpecAndSupplierAndQuantity.Price);
        }
    }
}
