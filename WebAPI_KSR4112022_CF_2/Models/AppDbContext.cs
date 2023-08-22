using Microsoft.EntityFrameworkCore;

namespace WebAPI_KSR4112022_CF_2.Models
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }
        public DbSet<Good> Goods { get; set; }
        public DbSet<Goodincome> Goodincomes { get; set; }
        public DbSet<Goodmove> Goodmoves { get; set; }
        public DbSet<Goodrest> Goodrests { get; set; } 
        public DbSet<Stock> Stocks { get; set; }

    }
}
