using Microsoft.EntityFrameworkCore;
using Pedruino.Hateoas.WebApi.Model;

namespace Pedruino.Hateoas.WebApi.Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Person> Persons { get; set; }
}