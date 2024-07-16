using BeerBuzz.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BeerBuzz.Infrastructure.Database;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Beer> Beers { get; set; }
}
