using BulletinBoard.Application.AppServices.Contexts.Attachment.Repositories;
using BulletinBoard.Application.AppServices.Contexts.Attachment.Services;
using BulletinBoard.Application.AppServices.Contexts.Post.Repositories;
using BulletinBoard.Application.AppServices.Contexts.Post.Services;
using BulletinBoard.Contracts.Attachment;
using BulletinBoard.Contracts.Post;
using BulletinBoard.Hosts.Api.Controllers;
using BulletinBoard.Infrastructure.DataAccess.Contexts.Post.Repositories;
using BulletinBoard.Infrastructure.DataAccess.Contexts.Attachment.Repositories;

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
                    typeof(AttachmentController)
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
            builder.Services.AddTransient<IAttachmentService, AttachmentService>();
            builder.Services.AddTransient<IAttachmentRepository, AttachmentRepository>();

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