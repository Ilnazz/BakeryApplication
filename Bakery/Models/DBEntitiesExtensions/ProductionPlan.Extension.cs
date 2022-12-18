using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bakery.Models
{
    public partial class ProductionPlan
    {
        public int TotalCount
        {
            get => ProductionPlan_Product.Sum(prodAndQuantity => prodAndQuantity.Quantity);
        }
    }
}
