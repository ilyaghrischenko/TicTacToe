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
        
        [HttpGet("getAppealedUsers")]
        public async Task<ActionResult<List<User>?>> GetAppealedUsers()
        {
            var users = await _adminControllerService.GetAppealedUsers();
            return Ok(users);
        }
        
        [HttpPost("getUserReports")]
        public async Task<ActionResult<List<Report>>> GetUserReports([FromBody] int userId)
        {
            var reports = await _adminControllerService.GetUserReports(userId);
            return Ok(reports);
        }
        
        [HttpPost("blockUser")]
        public async Task<IActionResult> BlockUser([FromBody] int userId)
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
