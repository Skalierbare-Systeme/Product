using Microsoft.EntityFrameworkCore;
using postsPraktikum.Models.Entities;

namespace postsPraktikum.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Post> Posts { get; set; }
    }
}
