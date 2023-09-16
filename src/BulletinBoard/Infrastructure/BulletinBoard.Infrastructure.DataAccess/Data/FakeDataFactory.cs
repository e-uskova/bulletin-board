namespace BulletinBoard.Infrastructure.DataAccess.Data
{
    public static class FakeDataFactory
    {
        public static List<Domain.Users.User> Data
        {
            get
            {
                return new List<Domain.Users.User>()
                {
                    new Domain.Users.User()
                    {
                        UserName = "John Doe",
                        UserEmail = "johndoe@example.com",
                        Password = "password",
                        Id = Guid.NewGuid(),
                    },
                    new Domain.Users.User()
                    {
                        UserName = "Jane Doe",
                        UserEmail = "janedoe@example.com",
                        Password = "drowssap",
                        Id = Guid.NewGuid(),
                    }
                };

            }
        }
    }
}
