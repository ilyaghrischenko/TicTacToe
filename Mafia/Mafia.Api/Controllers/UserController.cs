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
                return Ok(_userControllerService.GetUser());
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