using Microsoft.EntityFrameworkCore;
using MovieManagement.Web.Models;

namespace MovieManagement.Web.Persistence;

public class MovieDbContext(DbContextOptions<MovieDbContext> options) : DbContext(options)
{
    public DbSet<Movie> Movies => Set<Movie>();


    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema("app");
        builder.ApplyConfigurationsFromAssembly(typeof(MovieDbContext).Assembly);
        base.OnModelCreating(builder);
    }
}
