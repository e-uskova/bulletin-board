using BulletinBoard.Application.AppServices.Contexts.Attachment.Services;
using BulletinBoard.Application.AppServices.Mapping;
using BulletinBoard.Contracts.Attachment;
using BulletinBoard.Contracts.Users;
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

        /// <summary>
        /// Инициализация экземпляра <see cref="AttachmentController"/>.
        /// </summary>
        /// <param name="attachmentService">Сервис работы с вложениями.</param>
        public AttachmentController(IAttachmentService attachmentService)
        {
            _attachmentService = attachmentService;
        }

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
        /// <param name="cancellationToken">Токен отмены.</param>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile attachment, Guid postId, CancellationToken cancellationToken)
        {
            /*var emailFromClaims = HttpContext?.User?.Claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.Email)?.Value;
            if (emailFromClaims == null)
            {
                return StatusCode(401);
            }

            var curUser = await _userService.GetFirstWhere(u => u.Email == emailFromClaims);*/

            var bytes = await GetBytesAsync(attachment, cancellationToken);
            var attachmentDto = new AttachmentDto
            {
                Content = bytes,
                ContentType = attachment.ContentType,
                Name = attachment.FileName,
            };

            var result = await _attachmentService.UploadAsync(attachmentDto, postId, cancellationToken);

            if (result == Guid.Empty)
            {
                return BadRequest("Объявление не найдено.");
            }

            return StatusCode((int)HttpStatusCode.Created, (result));
        }

        /// <summary>
        /// Скачивание файла.
        /// </summary>
        /// <param name="id">Ижентификатор файла.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        [HttpGet("{id}")]
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

        private async Task<byte[]> GetBytesAsync(IFormFile attachment, CancellationToken cancellationToken)
        {
            var ms = new MemoryStream();
            await attachment.CopyToAsync(ms, cancellationToken);
            return ms.ToArray();
        }
    }
}
