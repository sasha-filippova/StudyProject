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
    /// Контроллер для управления отчетами.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly ProjectManagementContext _context;

        private readonly ReportRepository _reportRepository;

        /// <summary>
        /// Конструктор для ReportRepository.
        /// </summary>
        /// <param name="reportRepository">Репозиторий отчетов.</param>
        public ReportsController(ReportRepository reportRepository)
        {
            
            _reportRepository = reportRepository;
        }

        /// <summary>
        /// Получить все отчеты.
        /// </summary>
        /// <param name="cancellationToken">Маркер отмены.</param>
        /// <returns>Список отчетов.</returns>
        [HttpGet]
        public async Task<List<Report>> GetAllReport(CancellationToken cancellationToken)
        {
            return await _reportRepository.GetAllReportsAsync();
        }

        /// <summary>
        /// Получить отчет по ID.
        /// </summary>
        /// <param name="id">Идентификатор отчета.</param>
        /// <param name="cancellationToken">Маркер отмены.</param>
        /// <returns>Отчет по указанному идентификатору.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Report>> GetReportById(int id, CancellationToken cancellationToken)
        {
            var _report = await _reportRepository.GetReportByIdAsync(id, cancellationToken);
            if (_report == null)
            {
                return NotFound();
            }
            return _report;
        }

        /// <summary>
        /// Добавить новый отчет.
        /// </summary>
        /// <param name="newReport">Новый отчет.</param>
        /// <param name="cancellationToken">Маркер отмены.</param>
        /// <returns>Добавленный отчет.</returns>
        [HttpPost]
        public async Task<ActionResult<Report>> AddReportAsync(Report newReport, CancellationToken cancellationToken)
        {
            await _reportRepository.AddReportAsync(newReport, cancellationToken);
            return CreatedAtAction(nameof(GetReportById), new { id = newReport.Id }, newReport);
        }

        /// <summary>
        /// Обновить существующий отчет по ID.
        /// </summary>
        /// <param name="id">Идентификатор отчета.</param>
        /// <param name="updatedReport">Обновленный отчет.</param>
        /// <param name="cancellationToken">Маркер отмены.</param>
        /// <returns>Выполнено.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReport(int id, Report updatedReport, CancellationToken cancellationToken)
        {
            if (id != updatedReport.Id)
            {
                return BadRequest();
            }
            await _reportRepository.UpdateReportAsync(updatedReport, cancellationToken);
            return NoContent();
        }

        /// <summary>
        /// Удалить отчет по ID.
        /// </summary>
        /// <param name="id">Идентификатор отчета.</param>
        /// <param name="cancellationToken">Маркер отмены.</param>
        /// <returns>Выполено.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReport(int id, CancellationToken cancellationToken)
        {
            var reportToDelete = await _reportRepository.GetReportByIdAsync(id, cancellationToken);
            if (reportToDelete == null)
            {
                return NotFound();
            }
            await _reportRepository.DeleteReportAsync(id, cancellationToken);
            return NoContent();
        }
    }
}
