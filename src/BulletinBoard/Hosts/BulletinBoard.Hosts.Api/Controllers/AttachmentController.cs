/*using BulletinBoard.Application.AppServices.Contexts.Attachment.Services;
using BulletinBoard.Contracts.Attachment;
using Microsoft.AspNetCore.Mvc;

namespace BulletinBoard.Hosts.Api.Controllers
{
    /// <summary>
    /// Контроллер для работы с вложениями.
    /// </summary>
    [ApiController]
    [Route("post/attachment")]
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
        /// Получение вложения по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор вложения.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        /// <returns>Модель вложения <see cref="AttachmentDto"/></returns>
        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await _attachmentService.GetByIdAsync(id, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Получение всех вложений.
        /// </summary>
        /// <param name="cancellationToken">Отмена операции.</param>
        /// <returns>Коллекция объявлений <see cref="AttachmentDto"/></returns>
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
        {
            return Ok();
        }

        /// <summary>
        /// Создание вложения.
        /// </summary>
        /// <param name="dto">Модель для создания вложения.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        /// <returns>Идентификатор созданной сущности./></returns>
        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateAttachmentDto dto, CancellationToken cancellationToken)
        {
            var modelId = await _attachmentService.CreateAsync(dto, cancellationToken);
            return Created(nameof(CreateAsync), modelId);
        }

        /// <summary>
        /// Редактирование вложения.
        /// </summary>
        /// <param name="dto">Модель для редактирования вложения.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        [HttpPut]
        public async Task<IActionResult> UpdateByIdAsync(AttachmentDto dto, CancellationToken cancellationToken)
        {
            return Ok();
        }

        /// <summary>
        /// Удаление вложения по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор вложения.</param>
        /// <param name="cancellationToken">Отмена операции.</param>
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            return Ok();
        }
    }
}
*/