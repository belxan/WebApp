using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.EntityFramework;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
   public DbSet<User> Users { get; set; }
}
