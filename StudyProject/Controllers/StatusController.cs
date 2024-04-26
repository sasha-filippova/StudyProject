using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudyProject;
using StudyProject.Models;
using StudyProject.Repositories;

namespace StudyProject.Controllers
{
    /// <summary>
    /// Контроллер для управления статусами.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly ProjectManagementContext _context;

       
        private readonly StatusRepository _statusRepository;
        /// <summary>
        /// Конструктор для StatusController.
        /// </summary>
        /// <param name="statusRepository">Репозиторий статусов.</param>
        public StatusController(StatusRepository statusRepository)
        {

            _statusRepository = statusRepository;
        }

        /// <summary>
        /// Получить все статусы.
        /// </summary>
        /// <param name="cancellationToken">Маркер отмены.</param>
        /// <returns>Список статусов.</returns>
        [HttpGet]
        public async Task<List<Status>> GetAllStatuses(CancellationToken cancellationToken)
        {
            return await _statusRepository.GetAllStatusesAsync(cancellationToken);
        }

        /// <summary>
        ///  Получить статус по ID.
        /// </summary>
        /// <param name="id">Идентификатор статуса.</param>
        /// <param name="cancellationToken">Маркер отмены.</param>
        /// <returns>Статус по указанному идентификатору.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Status>> GetStatusById(int id, CancellationToken cancellationToken)
        {
            var _status = await _statusRepository.GetStatusByIdAsync(id, cancellationToken);
            if (_status == null)
            {
                return NotFound();
            }
            return _status;
        }

        /// <summary>
        /// Добавить новый статус.
        /// </summary>
        /// <param name="newStatus">Новый статус.</param>
        /// <param name="cancellationToken">Маркер отмены.</param>
        /// <returns>Добавленный статус.</returns>
        [HttpPost]
        public async Task<ActionResult<Status>> AddStatusAsync(Status newStatus, CancellationToken cancellationToken)
        {
            await _statusRepository.AddStatusAsync(newStatus, cancellationToken);
            return CreatedAtAction(nameof(GetStatusById), new { id = newStatus.Id }, newStatus);
        }

        /// <summary>
        /// Обновить существующий статус по ID.
        /// </summary>
        /// <param name="id">Идентификатор статуса.</param>
        /// <param name="updatedStatus">Обновленный статус.</param>
        /// <param name="cancellationToken">Маркер отмены.</param>
        /// <returns>Выполнено.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStatus(int id, Status updatedStatus, CancellationToken cancellationToken)
        {
            if (id != updatedStatus.Id)
            {
                return BadRequest();
            }
            await _statusRepository.UpdateStatusAsync(updatedStatus, cancellationToken);
            return NoContent();
        }

        /// <summary>
        /// Удалить статус по ID.
        /// </summary>
        /// <param name="id">Идентификатор статуса.</param>
        /// <param name="cancellationToken">Маркер отмены.</param>
        /// <returns>Выполнено.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStatus(int id, CancellationToken cancellationToken)
        {
            var statusToDelete = await _statusRepository.GetStatusByIdAsync(id, cancellationToken);
            if (statusToDelete == null)
            {
                return NotFound();
            }
            await _statusRepository.DeleteStatusAsync(id, cancellationToken);
            return NoContent();
        }
    }
}
