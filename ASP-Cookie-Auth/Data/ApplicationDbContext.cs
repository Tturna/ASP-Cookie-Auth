using ASP_Cookie_Auth.Models;
using Microsoft.EntityFrameworkCore;

namespace ASP_Cookie_Auth.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options) { }
    
    public DbSet<UserModel> Users { get; set; }
}