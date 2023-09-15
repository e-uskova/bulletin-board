using BulletinBoard.Application.AppServices.Contexts.Attachment.Repositories;
using BulletinBoard.Application.AppServices.Contexts.Attachment.Services;
using BulletinBoard.Application.AppServices.Contexts.Category.Repositories;
using BulletinBoard.Application.AppServices.Contexts.Category.Services;
using BulletinBoard.Application.AppServices.Contexts.Post.Repositories;
using BulletinBoard.Application.AppServices.Contexts.Post.Services;
using BulletinBoard.Application.AppServices.Contexts.User.Repositories;
using BulletinBoard.Application.AppServices.Contexts.User.Services;
using BulletinBoard.Contracts.Attachment;
using BulletinBoard.Contracts.Categories;
using BulletinBoard.Contracts.Post;
using BulletinBoard.Contracts.Users;
using BulletinBoard.Hosts.Api.Authentication;
using BulletinBoard.Hosts.Api.Controllers;
using BulletinBoard.Infrastructure.DataAccess.Contexts.Attachment.Repositories;
using BulletinBoard.Infrastructure.DataAccess.Contexts.Category.Repositories;
using BulletinBoard.Infrastructure.DataAccess.Contexts.Post.Repositories;
using BulletinBoard.Infrastructure.DataAccess.Contexts.User.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

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
                    typeof(CreateAttachmentDto),
                    typeof(AttachmentController),
                    typeof(UserDto),
                    typeof(CreateUserDto),
                    typeof(UserController),
                    typeof(CategoryDto),
                    typeof(CreateCategoryDto),
                    typeof(CategoryController)
                };

                foreach (var marker in includeDocsTypesMarkers)
                {
                    var xmlName = $"{marker.Assembly.GetName().Name}.xml";
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlName);

                    if (File.Exists(xmlPath))
                        s.IncludeXmlComments(xmlPath);
                }
            });

            #region Authentication

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                    options.SlidingExpiration = true;
                    options.Events.OnSignedIn = context =>
                    {
                        return Task.CompletedTask;
                    };
                })
                .AddScheme<AuthSchemeOptions, AuthSchemeHandler>("CustomScheme", options => { });

            #endregion

            #region Authorization

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("CustomPolicy", policy =>
                {
                    policy.RequireRole("Admin");
                    policy.RequireClaim("User", "User");
                });
            });

            #endregion

            builder.Services.AddTransient<IPostService, PostService>();
            builder.Services.AddTransient<IPostRepository, PostRepository>();
            builder.Services.AddTransient<IAttachmentService, AttachmentService>();
            builder.Services.AddTransient<IAttachmentRepository, AttachmentRepository>();
            builder.Services.AddTransient<IUserService, UserService>();
            builder.Services.AddTransient<IUserRepository, UserRepository>();
            builder.Services.AddTransient<ICategoryService, CategoryService>();
            builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            /*if (app.Environment.IsDevelopment())
            {*/
                app.UseSwagger();
                app.UseSwaggerUI();
            /*}*/

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}