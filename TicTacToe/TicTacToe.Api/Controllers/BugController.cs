using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicTacToe.Api.Filters;
using TicTacToe.Contracts.Controllers;
using TicTacToe.Domain.DbModels;
using TicTacToe.Domain.Enums;
using TicTacToe.DTO.Models;

namespace TicTacToe.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class BugController(IBugControllerService bugControllerService) : ControllerBase
    {
        private readonly IBugControllerService _bugControllerService = bugControllerService;
        
        [Role(Role.User)]
        [HttpPost("sendBug")]
        public async Task<IActionResult> SendBug([FromBody] BugModel bug)
        {
            _bugControllerService.GetErrors(ModelState);
            await _bugControllerService.SendBugAsync(bug);
            return Ok();
        }
        
        [Role(Role.Admin)]
        [HttpGet("getBugs")]
        public async Task<List<object>> GetAllBugs()
        {
            var response = await _bugControllerService.GetAllBugsAsync();
            return response;
        }
        
        [Role(Role.Admin)]
        [HttpPost("getBugsByStatus")]
        public async Task<List<object>> GetBugsByStatus(int status)
        {
            var response = await _bugControllerService.GetBugsByStatusAsync(status);
            return response;
        }
    }
}
