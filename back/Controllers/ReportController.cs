using Back.Models;
using Back.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;
        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        // GET: api/<ReportController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var reports = await _reportService.GetValues();
            return Ok(reports);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var report = await _reportService.GetValue(id);
            return Ok(report);
        }

        // POST api/<ReportController>
        [HttpPost]
        public async Task Post([FromBody] string value)
        {
            await _reportService.AddValue(new ReportData { Name = value });
        }


        // PUT api/<ReportController>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] string value)
        {
            await _reportService.UpdateValue(new ReportData { Id = id, Name = value });
        }


        // DELETE api/<ReportController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _reportService.DeleteValue(id);
            return Ok();
        }
    }
}
