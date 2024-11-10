using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicTacToe.Api.Filters;
using TicTacToe.Contracts.Interfaces.Controllers;
using TicTacToe.Domain.Enums;
using TicTacToe.DTO.Models;

namespace TicTacToe.Api.Controllers
{
    [Role(Role.User)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class BugController(IBugControllerService bugControllerService) : ControllerBase
    {
        private readonly IBugControllerService _bugControllerService = bugControllerService;
        
        [HttpPost]
        public async Task<IActionResult> SendBug([FromBody] BugModel bug)
        {
            await _bugControllerService.SendBugAsync(bug);
            return Ok();
        }
    }
}
