using Microsoft.EntityFrameworkCore;
using MovieManagement.Domain.Core;

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


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
             .UseAsyncSeeding(async (context, _, cancellationToken) =>
             {
                 var sampleMovies = new List<Movie> {
                     Movie.Create("Rob� Selvagem",
                                                "Anima��o",
                                                new DateTimeOffset(new DateTime(2024, 10, 14), TimeSpan.Zero),
                                                8),
                     Movie.Create("A Forja - O Poder da Transforma��o",
                           "Drama",
                           new DateTimeOffset(new DateTime(2024, 9, 26), TimeSpan.Zero),
                           7.5),

                     Movie.Create("Duna: Parte 2",
                           "Fic��o Cient�fica",
                           new DateTimeOffset(new DateTime(2024, 2, 29), TimeSpan.Zero),
                           8.6),
                     Movie.Create("Divertida Mente 2",
                           "Anima��o",
                           new DateTimeOffset(new DateTime(2024, 7, 20), TimeSpan.Zero),
                           7.9)
                     ,
                     Movie.Create("Capit�o Am�rica: Admir�vel Mundo Novo\r\n",
                           "Fic��o cient�fica",
                           new DateTimeOffset(new DateTime(2025, 2, 14), TimeSpan.Zero),
                           7.9)

                     };

                 await context.Set<Movie>().AddRangeAsync(sampleMovies, cancellationToken);
                 await context.SaveChangesAsync(cancellationToken);
             })
             .UseSeeding((context, _) =>
             {
                 var sampleMovies = new List<Movie> {
                     Movie.Create("Rob� Selvagem",
                                                "Anima��o",
                                                new DateTimeOffset(new DateTime(2024, 10, 14), TimeSpan.Zero),
                                                8),
                     Movie.Create("A Forja - O Poder da Transforma��o",
                           "Drama",
                           new DateTimeOffset(new DateTime(2024, 9, 26), TimeSpan.Zero),
                           7.5),

                     Movie.Create("Duna: Parte 2",
                           "Fic��o Cient�fica",
                           new DateTimeOffset(new DateTime(2024, 2, 29), TimeSpan.Zero),
                           8.6),
                     Movie.Create("Divertida Mente 2",
                           "Anima��o",
                           new DateTimeOffset(new DateTime(2024, 7, 20), TimeSpan.Zero),
                           7.9)
                     ,
                     Movie.Create("Capit�o Am�rica: Admir�vel Mundo Novo\r\n",
                           "Fic��o cient�fica",
                           new DateTimeOffset(new DateTime(2025, 2, 14), TimeSpan.Zero),
                           7.9)

                     };

                 context.Set<Movie>().AddRange(sampleMovies);
                 context.SaveChanges();
             });
    }
}
