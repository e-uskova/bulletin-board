using BulletinBoard.Domain;
using BulletinBoard.Infrastructure.DataAccess.Configurations;
using Microsoft.EntityFrameworkCore;

namespace BulletinBoard.Infrastructure.DataAccess
{
    /// <summary>
    /// Контекст БД.
    /// </summary>
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Attachment> Attachments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*modelBuilder.Entity<User>().Navigation(u => u.Posts).AutoInclude();

            modelBuilder.Entity<Post>().HasOne(a => a.Author).WithMany(p => p.Posts);
            modelBuilder.Entity<Post>().Navigation(p => p.Author).AutoInclude();
            modelBuilder.Entity<Post>().Navigation(p => p.Category).AutoInclude();

            modelBuilder.Entity<Attachment>().HasOne(a => a.Post).WithMany(p => p.Attachments);
            modelBuilder.Entity<Post>().Navigation(p => p.Attachments).AutoInclude();

            modelBuilder.Entity<Category>().Navigation(c => c.ParentCategory).AutoInclude();            
            modelBuilder.Entity<Category>();*/

            modelBuilder.ApplyConfiguration(new PostConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
