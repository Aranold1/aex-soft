using System.Data.Entity;
public class PostgresContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Manager> Managers { get; set; }
    public DbSet<Order> Orders { get; set; }
    
}