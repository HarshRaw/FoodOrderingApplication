

using ProjectApplication.Models;
using Microsoft.EntityFrameworkCore;
namespace ProjectApplication.Data
{
    public class ProjectDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<DishCategory> DishCategories { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }    


        public ProjectDbContext(DbContextOptions<ProjectDbContext> options):base(options)
        {

        }
        

    }
}
