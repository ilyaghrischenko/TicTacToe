using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicTacToe.Domain.DbModels;
using TicTacToe.Domain.Interfaces.Controllers;

namespace TicTacToe.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController(IAdminControllerService adminControllerService) : ControllerBase
    {
        private readonly IAdminControllerService _adminControllerService = adminControllerService;
        
        [HttpGet("getUserReports")]
        public async Task<ActionResult<List<Report>>> GetUserReports(int userId)
        {
            var reports = await _adminControllerService.GetUserReports(userId);
            return Ok(reports);
        }
        
        [HttpPost("blockUser")]
        public async Task<IActionResult> BlockUser(int userId)
        {
            try
            {
                await _adminControllerService.BlockUser(userId);
                return Ok();
            }
            catch (ArgumentNullException)
            {
                return BadRequest();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
