using Microsoft.EntityFrameworkCore;
using OFX.Entities;

namespace OFX.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);           
        }
        public DbSet<Transacoes> Transacoes { get; set; }

    }
}
