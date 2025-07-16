using AiAdventure.Backend.Data;
using AiAdventure.Backend.Dtos;
using AiAdventure.Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace aiAdventureControllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly AiAdventureDbContext _dbContext;

        public UserController(AiAdventureDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("create")]
        public async Task<ActionResult> Create([FromBody] string username)
        {
            var newPlayer = new PlayerProfile{ Username = username };
            
            _dbContext.Add(newPlayer);
            await _dbContext.SaveChangesAsync();
            return Ok($"Player {newPlayer.Username} created");
        }
    }
}
