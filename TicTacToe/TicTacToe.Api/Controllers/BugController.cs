using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicTacToe.Api.Filters;
using TicTacToe.Contracts.Controllers;
using TicTacToe.Domain.DbModels;
using TicTacToe.Domain.Enums;
using TicTacToe.DTO.Requests;
using TicTacToe.DTO.Responses;

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
        public async Task<IActionResult> SendBug([FromBody] SendBugRequest sendBug)
        {
            _bugControllerService.GetErrors(ModelState);
            await _bugControllerService.SendBugAsync(sendBug);
            return Ok();
        }
        
        [Role(Role.Admin)]
        [HttpGet("getBugs")]
        public async Task<ActionResult<List<BugResponse>>> GetAllBugs()
        {
            var response = await _bugControllerService.GetAllBugsAsync();
            return Ok(response);
        }
        
        [Role(Role.Admin)]
        [HttpPost("getBugsByStatus")]
        public async Task<ActionResult<List<BugResponse>>> GetBugsByStatus([FromBody] int status)
        {
            var response = await _bugControllerService.GetBugsByStatusAsync(status);
            return Ok(response);
        }

        [Role(Role.Admin)]
        [HttpPost("changeBugStatus")]
        public async Task<IActionResult> ChangeBugStatus([FromBody] BugChangeStatusRequest request)
        {
            await _bugControllerService.ChangeBugStatusAsync(request);
            return Ok();
        }
    }
}
