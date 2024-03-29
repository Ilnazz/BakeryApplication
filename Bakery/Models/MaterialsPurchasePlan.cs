//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Bakery.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class MaterialsPurchasePlan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MaterialsPurchasePlan()
        {
            this.Materials = new HashSet<Material>();
            this.MaterialsPurchasePlan_SupplierAndMaterialSpecification = new HashSet<MaterialsPurchasePlan_SupplierAndMaterialSpecification>();
            this.Employees = new HashSet<Employee>();
        }
    
        public int Id { get; set; }
        public int PlanStateId { get; set; }
        public System.DateTime DateTime { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Material> Materials { get; set; }
        public virtual PlanState PlanState { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MaterialsPurchasePlan_SupplierAndMaterialSpecification> MaterialsPurchasePlan_SupplierAndMaterialSpecification { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
