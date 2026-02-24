using Microsoft.EntityFrameworkCore;
using OrderApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderApi.Infrastructure.Data
{
    public class OrderDbContext(DbContextOptions<OrderDbContext> options) : DbContext(options)
    {

        public DbSet<Order> Orders { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //    // Configure the Order entity
        //    modelBuilder.Entity<Order>(entity =>
        //    {
        //        entity.HasKey(e => e.Id); // Set Id as the primary key
        //        entity.Property(e => e.ClientId)
        //            .IsRequired(); // Make CustomerId required
        //        entity.Property(e => e.OrderDate) 
        //            .IsRequired(); // Make OrderDate required
        //        entity.Property(e => e.PurchaseQuantity)
        //            .HasColumnType("decimal(18,2)"); // Set TotalAmount to have a specific decimal type
        //    });
        //}
    }
}
