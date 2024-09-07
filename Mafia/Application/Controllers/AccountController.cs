using Application.Models;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(IUserRepository userRepository) : ControllerBase
    {
        private readonly IUserRepository _userRepository = userRepository;

        //TODO: create services and implement them to controller
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            var user = await _userRepository.GetUser(loginModel.login);
            if (user == null) return NotFound("User not found");
            
            if (user.Password != loginModel.password) return NotFound("Wrong password");
            
            return Ok();
        }
    }
}