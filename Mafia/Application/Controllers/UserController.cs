using Domain.DbModels;
using Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserRepository userRepository) : ControllerBase
    {
        private readonly IUserRepository _userRepository = userRepository;
        
        [HttpGet("getUser")]
        public async Task<ActionResult<User>> GetUser(int userId)
        {
            var user = await _userRepository.Get(userId);
            if (user == null)
            {
                return NotFound(user);
            }
            
            return Ok(user);
        }
    }
}
