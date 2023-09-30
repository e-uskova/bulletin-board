using BulletinBoard.Application.AppServices.Abstractions.Repositories;
using BulletinBoard.Application.AppServices.Contexts.Attachment.Repositories;
//using BulletinBoard.Application.AppServices.Contexts.Attachment.Services;
//using BulletinBoard.Application.AppServices.Contexts.Category.Repositories;
//using BulletinBoard.Application.AppServices.Contexts.Category.Services;
using BulletinBoard.Application.AppServices.Contexts.Post.Repositories;
using BulletinBoard.Application.AppServices.Contexts.Post.Services;
using BulletinBoard.Application.AppServices.Contexts.User.Repositories;
using BulletinBoard.Application.AppServices.Contexts.User.Services;
using BulletinBoard.Contracts.Attachment;
//using BulletinBoard.Contracts.Categories;
using BulletinBoard.Contracts.Post;
using BulletinBoard.Contracts.Users;
using BulletinBoard.Hosts.Api.Controllers;
using BulletinBoard.Infrastructure.DataAccess.Base;
using BulletinBoard.Infrastructure.DataAccess.Data;
using BulletinBoard.Infrastructure.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace BulletinBoard.Hosts.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(s =>
            {
                var includeDocsTypesMarkers = new[]
                {
                    typeof(PostDto),
                    typeof(CreatePostDto),
                    typeof(PostController),
                    typeof(AttachmentDto),
                    //typeof(CreateAttachmentDto),
                    //typeof(AttachmentController),
                    typeof(UserDto),
                    typeof(CreateUserDto),
                    typeof(UserController),
                    //typeof(CategoryDto),
                    //typeof(CreateCategoryDto),
                    //typeof(CategoryController)
                };

                foreach (var marker in includeDocsTypesMarkers)
                {
                    var xmlName = $"{marker.Assembly.GetName().Name}.xml";
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlName);

                    if (File.Exists(xmlPath))
                        s.IncludeXmlComments(xmlPath);
                }
            });

            builder.Services.AddTransient<IPostService, PostService>();
            builder.Services.AddTransient<IPostRepository, PostRepository>();
            //builder.Services.AddTransient<IAttachmentService, AttachmentService>();
            builder.Services.AddTransient<IAttachmentRepository, AttachmentRepository>();
            builder.Services.AddTransient<IUserService, UserService>();
            builder.Services.AddTransient<IUserRepository, UserRepository>();
            //builder.Services.AddTransient<ICategoryService, CategoryService>();
            //builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();

            #region DB

            builder.Services.AddScoped(typeof(IRepository<>), typeof(EFRepository<>));
            builder.Services.AddScoped<IDbInitializer, EFDbInitializer>();

            builder.Services.AddDbContext<DataContext>(x =>
            {
                x.UseNpgsql(builder.Configuration.GetConnectionString("EFCoreDb"));
            });

            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            /*if (app.Environment.IsDevelopment())
            {*/
                app.UseSwagger();
                app.UseSwaggerUI();
            /*}*/

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}