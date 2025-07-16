using AiAdventure.Backend.Data;
using AiAdventure.Backend.Dtos;
using AiAdventure.Backend.Models;
using AiAdventure.Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace aiAdventureControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly AiAdventureDbContext _dbContext;

        public UserController(AiAdventureDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("{username}")]
        public async Task<ActionResult> Create([FromRoute] string username)
        {
            Console.WriteLine(username);
            var newPlayer = new PlayerProfile { Username = username };

            _dbContext.Add(newPlayer);
            await _dbContext.SaveChangesAsync();

            DatabaseLogger.LogDatabaseState(_dbContext);

            return Ok(newPlayer);
        }
    }
}
