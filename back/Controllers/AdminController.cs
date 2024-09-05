using Microsoft.AspNetCore.Mvc;
using Back.Services;

namespace Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {

        private readonly IAdminService _adminService;
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpPut("approveApplication")]
        public async Task<IActionResult> ApproveFarmer(int No)
        {
            try
            {
                var result = await _adminService.ApproveApplication(No);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("rejectApplication")]
        public async Task<IActionResult> RejectFarmer(int No)
        {
            try
            {
                var result = await _adminService.RejectApplication(No);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
