﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DBEntities : DbContext
    {
        public DBEntities()
            : base("name=DBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Gender> Genders { get; set; }
        public virtual DbSet<Material> Materials { get; set; }
        public virtual DbSet<MaterialSpecification> MaterialSpecifications { get; set; }
        public virtual DbSet<MaterialsPurchasePlan> MaterialsPurchasePlans { get; set; }
        public virtual DbSet<MaterialsPurchasePlan_SupplierAndMaterialSpecification> MaterialsPurchasePlan_SupplierAndMaterialSpecification { get; set; }
        public virtual DbSet<MeasureUnit> MeasureUnits { get; set; }
        public virtual DbSet<PlanState> PlanStates { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductIngredient> ProductIngredients { get; set; }
        public virtual DbSet<ProductionPlan> ProductionPlans { get; set; }
        public virtual DbSet<ProductionPlan_Product> ProductionPlan_Product { get; set; }
        public virtual DbSet<ProductSpecification> ProductSpecifications { get; set; }
        public virtual DbSet<Realization> Realizations { get; set; }
        public virtual DbSet<Realization_Product> Realization_Product { get; set; }
        public virtual DbSet<Realization_ResponsibleEmployees> Realization_ResponsibleEmployees { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }
}
