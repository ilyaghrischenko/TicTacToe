using Mafia.Domain.DbModels;
using Mafia.Domain.Interfaces.Controllers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mafia.Api.Controllers
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
                return Ok(await _userControllerService.GetUser());
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getFriends")]
        public async Task<ActionResult<List<Friend>?>> GetFriends()
        {
            try
            {
                return Ok(await _userControllerService.GetFriends());
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getAllUsers")]
        public async Task<ActionResult<List<User>?>> GetAllUsers()
        {
            try
            {
                return Ok(await _userControllerService.GetAllUsers());
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}