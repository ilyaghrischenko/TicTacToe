using TicTacToe.Domain.DbModels;
using TicTacToe.Domain.Interfaces.Controllers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TicTacToe.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserControllerService userControllerService) : ControllerBase
    {
        private readonly IUserControllerService _userControllerService = userControllerService;

        [HttpGet("getUser")]
        public async Task<ActionResult<User>> GetUser()
        {
            try
            {
                var user = await _userControllerService.GetUser();
                return user;
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("getUserStatistics")]
        public async Task<ActionResult<Statistic>> GetUserStatistics()
        {
            try
            {
                var userStatistics = await _userControllerService.GetUserStatistics();
                return Ok(userStatistics);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("getFriends")]
        public async Task<ActionResult<List<User>?>> GetFriends()
        {
            try
            {
                var friends = await _userControllerService.GetFriends();
                return Ok(friends);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("getAllUsers")]
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {
            try
            {
                var users = await _userControllerService.GetAllUsers();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

    }
}