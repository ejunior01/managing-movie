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
                     Movie.Create("Robô Selvagem",
                                                "Animação",
                                                new DateTimeOffset(new DateTime(2024, 10, 14), TimeSpan.Zero),
                                                8),
                     Movie.Create("A Forja - O Poder da Transformação",
                           "Drama",
                           new DateTimeOffset(new DateTime(2024, 9, 26), TimeSpan.Zero),
                           7.5),

                     Movie.Create("Duna: Parte 2",
                           "Ficção Científica",
                           new DateTimeOffset(new DateTime(2024, 2, 29), TimeSpan.Zero),
                           8.6),
                     Movie.Create("Divertida Mente 2",
                           "Animação",
                           new DateTimeOffset(new DateTime(2024, 7, 20), TimeSpan.Zero),
                           7.9)
                     ,
                     Movie.Create("Capitão América: Admirável Mundo Novo\r\n",
                           "Ficção científica",
                           new DateTimeOffset(new DateTime(2025, 2, 14), TimeSpan.Zero),
                           7.9)

                     };

                 await context.Set<Movie>().AddRangeAsync(sampleMovies, cancellationToken);
                 await context.SaveChangesAsync(cancellationToken);
             })
             .UseSeeding((context, _) =>
             {
                 var sampleMovies = new List<Movie> {
                     Movie.Create("Robô Selvagem",
                                                "Animação",
                                                new DateTimeOffset(new DateTime(2024, 10, 14), TimeSpan.Zero),
                                                8),
                     Movie.Create("A Forja - O Poder da Transformação",
                           "Drama",
                           new DateTimeOffset(new DateTime(2024, 9, 26), TimeSpan.Zero),
                           7.5),

                     Movie.Create("Duna: Parte 2",
                           "Ficção Científica",
                           new DateTimeOffset(new DateTime(2024, 2, 29), TimeSpan.Zero),
                           8.6),
                     Movie.Create("Divertida Mente 2",
                           "Animação",
                           new DateTimeOffset(new DateTime(2024, 7, 20), TimeSpan.Zero),
                           7.9)
                     ,
                     Movie.Create("Capitão América: Admirável Mundo Novo\r\n",
                           "Ficção científica",
                           new DateTimeOffset(new DateTime(2025, 2, 14), TimeSpan.Zero),
                           7.9)

                     };

                 context.Set<Movie>().AddRange(sampleMovies);
                 context.SaveChanges();
             });
    }
}
