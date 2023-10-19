using BulletinBoard.Application.AppServices.Contexts.Attachment.Services;
using BulletinBoard.Application.AppServices.Mapping;
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

        /*[HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = _attachmentService.GetAllAsync(cancellationToken);
            if (result == null)
            {
                return NotFound();
            }

            File[] files = File[];
            foreach (var attachment in result)
            {
                files.Add(File(attachment.Content, attachment.ContentType, attachment.Name));
            }
            return Ok();
        }*/

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
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile attachment, CancellationToken cancellationToken)
        {
            var bytes = await GetBytesAsync(attachment, cancellationToken);
            var attachmentDto = new AttachmentDto
            {
                Content = bytes,
                ContentType = attachment.ContentType,
                Name = attachment.FileName,
            };

            var result = await _attachmentService.UploadAsync(attachmentDto, cancellationToken);
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
