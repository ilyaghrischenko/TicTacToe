using TicTacToe.Domain.DbModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicTacToe.Api.Filters;
using TicTacToe.Contracts.Interfaces.Controllers;
using TicTacToe.Domain.Enums;

namespace TicTacToe.Api.Controllers
{
    [Role(Role.User)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserControllerService userControllerService) : ControllerBase
    {
        private readonly IUserControllerService _userControllerService = userControllerService;

        [HttpGet("getUser")]
        public async Task<ActionResult<User>> GetUser()
        {
            var user = await _userControllerService.GetUserAsync();
            return Ok(user);
        }

        [HttpGet("getUserStatistics")]
        public async Task<ActionResult<Statistic>> GetUserStatistics()
        {
            var userStatistics = await _userControllerService.GetUserStatisticsAsync();
            return Ok(userStatistics);
        }

        [HttpGet("getFriends")]
        public async Task<ActionResult<List<User>?>> GetFriends()
        {
            var friends = await _userControllerService.GetFriendsAsync();
            return Ok(friends);
        }

        [HttpGet("getAllUsers")]
        public async Task<ActionResult<List<User>?>> GetAllUsers()
        {
            var users = await _userControllerService.GetAllUsersAsync();
            return Ok(users);
        }
    }
}