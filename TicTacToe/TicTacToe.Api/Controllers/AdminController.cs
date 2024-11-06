using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicTacToe.Api.Filters;
using TicTacToe.Domain.DbModels;
using TicTacToe.Domain.Enums;
using TicTacToe.Domain.Interfaces.Controllers;

namespace TicTacToe.Api.Controllers
{
    [Role(Role.Admin)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController(IAdminControllerService adminControllerService) : ControllerBase
    {
        private readonly IAdminControllerService _adminControllerService = adminControllerService;

        [HttpGet("getAppealedUsers")]
        public async Task<ActionResult<List<User>?>> GetAppealedUsers()
        {
            var users = await _adminControllerService.GetAppealedUsersAsync();
            return Ok(users);
        }

        [HttpPost("getUserReports")]
        public async Task<ActionResult<List<Report>>> GetUserReports([FromBody] int userId)
        {
            var reports = await _adminControllerService.GetUserReportsAsync(userId);
            return Ok(reports);
        }

        [HttpPost("blockUser")]
        public async Task<IActionResult> BlockUser([FromBody] int userId)
        {
            await _adminControllerService.BlockUserAsync(userId);
            return Ok();
        }
    }
}