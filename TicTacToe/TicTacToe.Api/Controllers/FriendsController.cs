using TicTacToe.Domain.Interfaces.Controllers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicTacToe.Api.Filters;

namespace TicTacToe.Api.Controllers
{
    [Role("User")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class FriendsController(IFriendsControllerService friendsControllerService) : ControllerBase
    {
        private readonly IFriendsControllerService _friendsControllerService = friendsControllerService;

        [HttpPost("addFriend")]
        public async Task<IActionResult> AddFriend([FromBody] int userId)
        {
            try
            {
                _friendsControllerService.GetErrors(ModelState);
                await _friendsControllerService.AddFriend(userId);
            }
            catch (KeyNotFoundException k)
            {
                return NotFound(k.Message);
            }
            catch (ArgumentException a)
            {
                return BadRequest(a.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

            return Ok();
        }

        [HttpPost("deleteFriend")]
        public async Task<IActionResult> DeleteFriend([FromBody] int userId)
        {
            try
            {
                _friendsControllerService.GetErrors(ModelState);
                await _friendsControllerService.DeleteFriend(userId);
            }
            catch (KeyNotFoundException k)
            {
                return NotFound(k.Message);
            }
            catch (ArgumentException a)
            {
                return BadRequest(a.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

            return Ok();
        }
    }
}