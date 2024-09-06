using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnalyticsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;


        public AnalyticsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("Total Analytics")]
        public async Task<IActionResult> GetTotalApplciations()
        {
            var AppliedApplciation = await _context.Applications
                .Include(a => a.Applicant)
                .GroupBy(a => a.CreatedAt)
                .Select(a=> new { Date = a.Key, Count = a.Count() })
                .ToListAsync();

            

            return Ok(AppliedApplciation);
        }
        [HttpGet("RedListed Analytics")]
        public async Task<IActionResult> GetTotalRedListedApplciations()
        {
            var AppliedApplciation = await _context.Applications
                .Include(a => a.Applicant)
                .Where(a=> a.Status.Contains("Red"))
                .GroupBy(a => a.CreatedAt)
                .Select(a => new { Date = a.Key, Count = a.Count() })
                .ToListAsync();



            return Ok(AppliedApplciation);
        }
        [HttpGet("YellowListed Analytics")]
        public async Task<IActionResult> GetTotalYellowListedApplciations()
        {
            var AppliedApplciation = await _context.Applications
                .Include(a => a.Applicant)
                .Where(a => a.Status.Contains("Yellow"))
                .GroupBy(a => a.CreatedAt)
                .Select(a => new { Date = a.Key, Count = a.Count() })
                .ToListAsync();
            return Ok(AppliedApplciation);
        }
        [HttpGet("UNListed Analytics")]
        public async Task<IActionResult> GetTotalUNListedApplciations()
        {
            var AppliedApplciation = await _context.Applications
                .Include(a => a.Applicant)
                .Where(a => a.Status.Contains("UN"))
                .GroupBy(a => a.CreatedAt)
                .Select(a => new { Date = a.Key, Count = a.Count() })
                .ToListAsync();
            return Ok(AppliedApplciation);
        }
        [HttpGet("Clear")]
        public async Task<IActionResult> GetClear()
        {
            var clearCount = await _context.Applications
                                .Include(a => a.Applicant)
                                .Where(a => a.Status == "Clear")
                                .CountAsync();

            return Ok(new { Count = clearCount });
        }

        [HttpGet("approved")]
        public async Task<IActionResult> GetApproved()
        {
            var acceptedCount = await _context.Applications
                                .Include(a => a.Applicant)
                                .Where(a => a.Status == "Approve")
                                .CountAsync();


            return Ok(new {Count = acceptedCount});
        }
        [HttpGet("rejected")]
        public async Task<IActionResult> GetRejected()
        {
            var rejectedCount = await _context.Applications
                                .Include(a => a.Applicant)
                                .Where(a => a.Status == "Reject")
                                .CountAsync();  

            return Ok(new { Count = rejectedCount });
        }
    }
}
