using BulletinBoard.Domain;
using Microsoft.EntityFrameworkCore;

namespace BulletinBoard.Infrastructure.DataAccess.Base
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

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
            //modelBuilder.Entity<User>().Navigation(u => u.Posts).AutoInclude();

            //modelBuilder.Entity<Post>().HasOne(a => a.Author).WithMany(p => p.Posts);
            //modelBuilder.Entity<Post>().Navigation(p => p.Author).AutoInclude();
            //modelBuilder.Entity<Post>().Navigation(p => p.Category).AutoInclude();

            modelBuilder.Entity<Attachment>().HasOne(a => a.Post).WithMany(p => p.Attachments);
            modelBuilder.Entity<Post>().Navigation(p => p.Attachments).AutoInclude();

            //modelBuilder.Entity<Category>().Navigation(c => c.ParentCategory).AutoInclude();            
            //modelBuilder.Entity<Category>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
