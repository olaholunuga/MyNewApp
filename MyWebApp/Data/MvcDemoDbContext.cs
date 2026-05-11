using Microsoft.EntityFrameworkCore;
using MyWebApp.Models.Domain;

namespace MyWebApp.Data;

public class MvcDemoDbContext(DbContextOptions<MvcDemoDbContext> options) : DbContext(options)
{
    public DbSet<Employee> Employees { get; set; } = default!;
}
