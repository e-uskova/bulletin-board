using BulletinBoard.Domain;

namespace BulletinBoard.Infrastructure.DataAccess.Data

{
    public static class FakeDataFactory
    {  
        public static List<User> Users
        {
            get
            {
                return new List<User>()
                {
                    new User()
                    {
                        Name = "John Doe",
                        Email = "johndoe@example.com",
                        Password = "password",
                        Posts = new List<Post>
                        {
                            Posts[0],
                            Posts[1]
                        },
                        Id = Guid.Parse("b8407a1e-f9e2-4a5d-9ca8-fdc794f6f490"),
                    },
                    new User()
                    {
                        Name = "Jane Doe",
                        Email = "janedoe@example.com",
                        Password = "drowssap",
                        Id = Guid.Parse("eeb6b7a5-78aa-4c68-8d1e-343e3aa7ebcb"),
                    },
                    new User()
                    {
                        Name = "George Smith",
                        Email = "gsmith@example.com",
                        Password = "SuperPass123",
                        Id = Guid.Parse("3c4e6912-0b65-4be4-8d7b-1a7024bdd2e5"),
                    }
                };
            }
        }

        public static List<Post> Posts
        {
            get
            {
                return new List<Post>()
                {
                    new Post()
                    {
                        Title = "Old books",
                        Description = "Ancient books from grandma. Huge collection.",
                        Attachments = new List<Attachment>
                        {
                            new Attachment()
                            {
                                Name = "books1",
                                Path = "img/books1.jpg",
                            },
                            new Attachment()
                            {
                                Name = "books2",
                                Path = "img/books2.jpg",
                            }
                        },
                        Category = Categories[0],
                        Price = 1500,
                        Id = Guid.Parse("6ddede03-d931-427a-9850-a6c4008c7e1c"),
                    },
                    new Post()
                    {
                        Title = "Chess",
                        Description = "I don't know how to use.",
                        Attachments = new List<Attachment>
                        {
                            new Attachment()
                            {
                                Name = "chess",
                                Path = "img/chess.jpg",
                            }
                        },
                        Category = Categories[0],
                        Price = 308,
                        Id = Guid.Parse("6eb34082-d213-4111-baa5-a79911ae32e4"),
                    },
                };
            }
        }

        public static List<Category> Categories
        {
            get
            {
                return new List<Category>()
                {
                    new Category()
                    {
                        Name = "Hobby",
                        ParentCategory = null,
                        Id = Guid.Parse("bcd5dbf2-89be-402e-9534-25cdf9fa3719"),
                    },
                };
            }
        }
    }
}