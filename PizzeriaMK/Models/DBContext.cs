using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace PizzeriaMK.Models
{
    public partial class DBContext : DbContext
    {
        public DBContext()
            : base("name=DBContext")
        {
        }

        public virtual DbSet<OrderItem> OrderItems { get; set; }
        public virtual DbSet<OrderSummary> OrderSummaries { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderSummary>()
                .HasMany(e => e.OrderItems)
                .WithRequired(e => e.OrderSummary)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.OrderItems)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.OrderSummaries)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);
        }
    }
}
