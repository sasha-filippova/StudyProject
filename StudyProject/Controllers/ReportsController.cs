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
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly ProjectManagementContext _context;

        private readonly ReportRepository _reportRepository;


        public ReportsController(ReportRepository reportRepository)
        {
            //_context = context;
            _reportRepository = reportRepository;
        }


        [HttpGet]
        public async Task<List<Report>> GetAllReport()
        {
            return await _reportRepository.GetAllReportsAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Report>> GetReportById(int id)
        {
            var _report = await _reportRepository.GetReportsById(id);
            if (_report == null)
            {
                return NotFound();
            }
            return _report;
        }

        [HttpPost]
        public async Task<ActionResult<Report>> AddReportAsync(Report newReport)
        {
            await _reportRepository.AddReportsAsync(newReport);
            return CreatedAtAction(nameof(GetReportById), new { id = newReport.ReportId }, newReport);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReport(int id, Report updatedReport)
        {
            if (id != updatedReport.ReportId)
            {
                return BadRequest();
            }
            await _reportRepository.UpdateReports(updatedReport);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReport(int id)
        {
            var reportToDelete = await _reportRepository.GetReportsById(id);
            if (reportToDelete == null)
            {
                return NotFound();
            }
            await _reportRepository.DeleteReports(id);
            return NoContent();
        }
    }
}
