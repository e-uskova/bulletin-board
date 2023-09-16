using Microsoft.EntityFrameworkCore;

namespace BulletinBoard.Infrastructure.DataAccess.Contexts.Base
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Domain.Users.User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Domain.Users.User>()
            //            .HasKey(u => u.Id)
            //            .WithOne(u => u.Category);

            base.OnModelCreating(modelBuilder);
        }
    }
}
