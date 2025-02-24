using Microsoft.EntityFrameworkCore;
using learn_asp.Models;

namespace learn_asp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        public DbSet<RegisterModel> RegisteredUsers { get; set; }
    }
}