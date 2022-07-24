using InstrumentStore.Models;
using Microsoft.EntityFrameworkCore;

namespace InstrumentStore.DbContexts;

/// <summary>
/// This is a DbContext class for the InstrumentStore.
/// </summary>
/// <author>lisa.l.crowell@gmail.com</author>
public class InstrumentStoreDbContext : DbContext
{
    public InstrumentStoreDbContext(DbContextOptions<InstrumentStoreDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<Product>? Products { get; set; }
    public DbSet<Customer>? Customers { get; set; }
    public DbSet<Order>? Orders { get; set; }
}