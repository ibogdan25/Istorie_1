﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Istorie
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class IstorieEntities : DbContext
    {
        public IstorieEntities()
            : base("name=IstorieEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Clase> Clases { get; set; }
        public virtual DbSet<Intrebari> Intrebaris { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Evenimente> Evenimentes { get; set; }
    }
}
