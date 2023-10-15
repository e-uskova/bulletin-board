using BulletinBoard.Application.AppServices.Contexts.Attachment.Services;
using BulletinBoard.Contracts.Attachment;
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

        /// <summary>
        /// Инициализация экземпляра <see cref="AttachmentController"/>.
        /// </summary>
        /// <param name="attachmentService">Сервис работы с вложениями.</param>
        public AttachmentController(IAttachmentService attachmentService)
        {
            _attachmentService = attachmentService;
        }

        /// <summary>
        /// Загрузка файла в систему.
        /// </summary>
        /// <param name="file">Файл.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file, CancellationToken cancellationToken)
        {
            var bytes = await GetBytesAsync(file, cancellationToken);
            var fileDto = new AttachmentDto
            {
                Content = bytes,
                ContentType = file.ContentType,
                Name = file.FileName,
            };

            var result = await _attachmentService.UploadAsync(fileDto, cancellationToken);
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

        private async Task<byte[]> GetBytesAsync(IFormFile file, CancellationToken cancellationToken)
        {
            var ms = new MemoryStream();
            await file.CopyToAsync(ms, cancellationToken);
            return ms.ToArray();
        }
    }
}
