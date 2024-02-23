using Microsoft.EntityFrameworkCore;
using TinyLink.API.Models;


namespace TinyLink.API.Infrastructure
{
    public class TinyLinkDbContext : DbContext
    {
        public DbSet<Models.TinyLink> TinyLinks { get; set; }
        public TinyLinkDbContext(DbContextOptions<TinyLinkDbContext> options) : base (options)
        {
            
        }
        public TinyLinkDbContext()
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TinyLink>().HasKey(t => t.Id);
   
        }
    }
}
