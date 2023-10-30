using BulletinBoard.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BulletinBoard.Infrastructure.DataAccess.Configurations
{
    /// <summary>
    /// Конфигурация таблицы Users.
    /// </summary>
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasData(
                new User
                {
                    Id = Guid.NewGuid(),
                    Name = "Администратор",
                    Email = "admin@admin.com",
                    Password = "admin",
                    Telephone = "0",
                });
        }
    }
}
