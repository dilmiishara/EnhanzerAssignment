using Enhanzer.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Enhanzer.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<LocationDetail> LocationDetails { get; set; }
}