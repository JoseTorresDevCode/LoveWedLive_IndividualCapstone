using System;
using System.Collections.Generic;
using System.Text;
using LoveWedLive_Capstone.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LoveWedLive_Capstone.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<QuoteRequest> QuoteRequests { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<VendorQuote> VendorQuotes { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
