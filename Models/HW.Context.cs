﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Homework.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class HWEntities : DbContext
    {
        public HWEntities()
            : base("name=HWEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<customer> customer { get; set; }
        public virtual DbSet<invoice> invoice { get; set; }
        public virtual DbSet<invoicedetail> invoicedetail { get; set; }
        public virtual DbSet<item> item { get; set; }
        public virtual DbSet<currency> currency { get; set; }
        public virtual DbSet<CITY> CITies { get; set; }
        public virtual DbSet<Flight> Flights { get; set; }
        public virtual DbSet<Schedule> Schedules { get; set; }
        public virtual DbSet<Seat> Seats { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<Customer_Sample> Customer_Sample { get; set; }
    }
}
