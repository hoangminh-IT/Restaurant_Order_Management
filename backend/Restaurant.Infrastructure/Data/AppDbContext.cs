using Microsoft.EntityFrameworkCore;

namespace Restaurant.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}