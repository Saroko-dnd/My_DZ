﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DateTimeEntityTest
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ModelWithDateTimeContainer : DbContext
    {
        public ModelWithDateTimeContainer()
            : base("name=ModelWithDateTimeContainer")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Peoples> PeoplesSet { get; set; }
        public virtual DbSet<Jobs> JobsSet { get; set; }
    }
}
