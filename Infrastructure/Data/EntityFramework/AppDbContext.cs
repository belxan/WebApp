using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.EntityFramework;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
}
