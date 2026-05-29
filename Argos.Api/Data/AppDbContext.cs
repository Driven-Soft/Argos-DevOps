using Microsoft.EntityFrameworkCore;
using Argos.Api.Models;

namespace Argos.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<ZonaRisco> ZonasRisco => Set<ZonaRisco>();
}