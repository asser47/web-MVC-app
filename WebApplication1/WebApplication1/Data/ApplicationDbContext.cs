using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options): base(options) 
        {
            
        }

        //protected ApplicationDbContext()
        //{
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<UserPermission>().ToTable("UserPermissions").HasKey(x => new {x.UserId, x.PermissionsId });
        }
    }
}
