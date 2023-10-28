using BulletinBoard.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace BulletinBoard.Hosts.DbMigrator
{
    public class MigrationDbContext : DataContext
    {
        public MigrationDbContext(DbContextOptions options) : base(options) { }
    }
}
