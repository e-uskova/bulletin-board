namespace BulletinBoard.Infrastructure.DataAccess.Data
{
    public class EFDbInitializer : IDbInitializer
    {
        private DataContext _dataContext;

        public EFDbInitializer(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void InitializeDb()
        {
            _dataContext.Database.EnsureDeleted();
            _dataContext.Database.EnsureCreated();

            // TODO
        }
    }
}