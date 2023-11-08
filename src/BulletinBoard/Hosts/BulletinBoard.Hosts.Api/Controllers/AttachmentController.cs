using BulletinBoard.Application.AppServices.Contexts.Attachment.Services;
using BulletinBoard.Application.AppServices.Contexts.Post.Services;
using BulletinBoard.Application.AppServices.Contexts.User.Services;
using BulletinBoard.Contracts.Attachment;
using BulletinBoard.Contracts.Categories;
using BulletinBoard.Contracts.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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
        /// Загрузка файла в систему.
        /// </summary>
        /// <param name="attachment">Файл.</param>
        /// <param name="postId">Идентификатор объявления.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        [Authorize]
        [ProducesResponseType(typeof(CategoryDto), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile attachment, Guid postId, CancellationToken cancellationToken)
        {
            var curUser = await GetCurrentUserAsync(cancellationToken);
            if (curUser == null)
            {
                return Unauthorized();
            }

            var postEntity = await _postService.GetByIdAsync(postId, cancellationToken);
            if (postEntity == null || postEntity.AuthorId != curUser.Id)
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
        [ProducesResponseType(typeof(CategoryDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
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
        [ProducesResponseType(typeof(CategoryDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAttachmentAsync(Guid id, CancellationToken cancellationToken)
        {
            var curUser = await GetCurrentUserAsync(cancellationToken);
            if (curUser == null)
            {
                return Unauthorized();
            }

            var attachment = await _attachmentService.GetInfoByIdAsync(id, cancellationToken);
            if (attachment == null)
            {
                return BadRequest();
            }

            var postEntity = await _postService.GetByIdAsync(attachment.PostId, cancellationToken);
            if (postEntity == null || postEntity.AuthorId != curUser.Id)
            {
                return BadRequest();
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

        /// <summary>
        /// Получение текущего пользователя.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Модель пользователя.</returns>
        private async Task<UserDto?> GetCurrentUserAsync(CancellationToken cancellationToken)
        {
            var idFromClaims = HttpContext?.User?.Claims?.FirstOrDefault(claim => claim.Type == "Id")?.Value;
            if (idFromClaims == null)
            {
                return null;
            }

            var curUser = await _userService.GetByIdAsync(Guid.Parse(idFromClaims), cancellationToken);
            return curUser;
        }
    }
}
