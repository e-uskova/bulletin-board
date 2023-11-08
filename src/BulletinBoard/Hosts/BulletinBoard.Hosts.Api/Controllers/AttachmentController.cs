using BulletinBoard.Application.AppServices.Contexts.Attachment.Services;
using BulletinBoard.Application.AppServices.Contexts.Post.Services;
using BulletinBoard.Application.AppServices.Contexts.User.Services;
using BulletinBoard.Contracts.Attachment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace BulletinBoard.Hosts.Api.Controllers
{
    /// <summary>
    /// Контроллер для работы с вложениями.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class AttachmentController : ControllerBase
    {
        private readonly IAttachmentService _attachmentService;
        private readonly IUserService _userService;
        private readonly IPostService _postService;

        /// <summary>
        /// Инициализация экземпляра <see cref="AttachmentController"/>.
        /// </summary>
        /// <param name="attachmentService">Сервис работы с вложениями.</param>
        /// <param name="userService">Сервис работы с пользователями.</param>
        /// <param name="postService">Сервис работы с объявлениями.</param>
        public AttachmentController(IAttachmentService attachmentService,
                                    IUserService userService,
                                    IPostService postService)
        {
            _attachmentService = attachmentService;
            _userService = userService;
            _postService = postService;
        }

        /// <summary>
        /// Получение инфо обо всех файлах.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllInfo(CancellationToken cancellationToken)
        {
            var result = await _attachmentService.GetAllInfoAsync(cancellationToken);                                                     
            return Ok(result);
        }

        /// <summary>
        /// Загрузка файла в систему.
        /// </summary>
        /// <param name="attachment">Файл.</param>
        /// <param name="postId">Идентификатор объявления.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile attachment, Guid postId, CancellationToken cancellationToken)
        {
            var emailFromClaims = HttpContext?.User?.Claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.Email)?.Value;
            if (emailFromClaims == null)
            {
                return Unauthorized();
            }
            var curUser = await _userService.GetFirstWhere(u => u.Email == emailFromClaims, cancellationToken);
            if (curUser == null)
            {
                return Unauthorized();
            }

            var post = await _postService.GetByIdAsync(postId, cancellationToken);
            if (post == null || post.AuthorName != curUser.Name)
            {
                return BadRequest();
            }

            var bytes = await GetBytesAsync(attachment, cancellationToken);
            var attachmentDto = new AttachmentDto
            {
                Content = bytes,
                ContentType = attachment.ContentType,
                Name = attachment.FileName,
            };

            var result = await _attachmentService.UploadAsync(attachmentDto, postId, cancellationToken);

            return result == Guid.Empty ? BadRequest() : StatusCode((int)HttpStatusCode.Created, result);
        }

        /// <summary>
        /// Скачивание файла.
        /// </summary>
        /// <param name="id">Ижентификатор файла.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Download(Guid id, CancellationToken cancellationToken)
        {
            var result = await _attachmentService.DownloadAsync(id, cancellationToken);
            if (result == null)
            {
                return NotFound();
            }

            Response.ContentLength = result.Content.Length;
            return File(result.Content, result.ContentType, result.Name);
        }

        /// <summary>
        /// Удаление файла.
        /// </summary>
        /// <param name="id">Идентификатор файла.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAttachmentAsync(Guid id, CancellationToken cancellationToken)
        {
            var emailFromClaims = HttpContext?.User?.Claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.Email)?.Value;
            if (emailFromClaims == null)
            {
                return Unauthorized();
            }
            var curUser = await _userService.GetFirstWhere(u => u.Email == emailFromClaims, cancellationToken);
            if (curUser == null)
            {
                return Unauthorized();
            }
            var file = await _attachmentService.GetInfoByIdAsync(id, cancellationToken);
            if (file != null && file.PostId != Guid.Empty)
            {
                var post = await _postService.GetByIdAsync(file.PostId, cancellationToken);
                if (post != null && post.AuthorName != curUser.Name)
                {
                    return BadRequest("Этот файл не из Вашего объявления");
                }
            }

            var result = await _attachmentService.DeleteAsync(id, cancellationToken);
            return result ? BadRequest() : Ok();
        }


        private async Task<byte[]> GetBytesAsync(IFormFile attachment, CancellationToken cancellationToken)
        {
            var ms = new MemoryStream();
            await attachment.CopyToAsync(ms, cancellationToken);
            return ms.ToArray();
        }
    }
}
