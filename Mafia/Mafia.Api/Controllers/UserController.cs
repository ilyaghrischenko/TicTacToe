using Mafia.Domain.DbModels;
using Mafia.Domain.Interfaces.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Mafia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserControllerService userControllerService) : ControllerBase
    {
        private readonly IUserControllerService _userControllerService = userControllerService;
        
        [HttpGet("getUser")]
        public async Task<ActionResult<User>> GetUser(int userId)
        {
            try
            {
                var user = await _userControllerService.GetUser(userId);
                return Ok(user);
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
