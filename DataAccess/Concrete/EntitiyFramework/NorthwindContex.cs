using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Contex : Db tabloları ile projedeki db sınıflarını birbirine bağlayan nesne
namespace DataAccess.Concrete.EntitiyFramework
{
    public class NorthwindContex:DbContext
    {
        //Ef Core Bağlantı sağlayacağı metot
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Bağlantı yolu
            optionsBuilder.UseSqlServer(@"Server=MSı\SQLEXPRESS;Database=NORTHWND;Trusted_Connection=true;TrustServerCertificate=true");
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
    }
}

