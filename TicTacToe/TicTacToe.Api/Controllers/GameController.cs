using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicTacToe.Domain.Interfaces.Controllers;

namespace TicTacToe.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class GameController(IGameControllerService gameControllerService) : ControllerBase
    {
        private readonly IGameControllerService _gameControllerService = gameControllerService;
        
        [HttpGet("win")]
        public async Task<IActionResult> Win()
        {
            try
            {
                var result = await _gameControllerService.Win();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("lose")]
        public async Task<IActionResult> Lose()
        {
            try
            {
                var result = await _gameControllerService.Lose();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
