using TicTacToe.Domain.Interfaces.Controllers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicTacToe.Api.Filters;
using TicTacToe.Domain.Enums;

namespace TicTacToe.Api.Controllers
{
    [Role(Role.User)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class FriendsController(IFriendsControllerService friendsControllerService) : ControllerBase
    {
        private readonly IFriendsControllerService _friendsControllerService = friendsControllerService;

        [HttpPost("addFriend")]
        public async Task<IActionResult> AddFriend([FromBody] int userId)
        {
            _friendsControllerService.GetErrors(ModelState);
            await _friendsControllerService.AddFriend(userId);
            return Ok();
        }

        [HttpPost("deleteFriend")]
        public async Task<IActionResult> DeleteFriend([FromBody] int userId)
        {
            _friendsControllerService.GetErrors(ModelState);
            await _friendsControllerService.DeleteFriend(userId);
            return Ok();
        }
    }
}