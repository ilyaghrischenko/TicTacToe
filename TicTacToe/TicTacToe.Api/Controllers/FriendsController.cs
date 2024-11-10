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
    public class FriendsController(IFriendsControllerService friendsControllerService) : ControllerBase
    {
        private readonly IFriendsControllerService _friendsControllerService = friendsControllerService;

        [HttpPost("addFriend")]
        public async Task<IActionResult> AddFriend([FromBody] int userId)
        {
            _friendsControllerService.GetErrors(ModelState);
            await _friendsControllerService.AddFriendAsync(userId);
            return Ok();
        }

        [HttpPost("deleteFriend")]
        public async Task<IActionResult> DeleteFriend([FromBody] int userId)
        {
            _friendsControllerService.GetErrors(ModelState);
            await _friendsControllerService.DeleteFriendAsync(userId);
            return Ok();
        }
    }
}