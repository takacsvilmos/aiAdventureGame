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
        public async Task<ActionResult<PlayerProfileDto>> Create([FromRoute] string username)
        {
            Console.WriteLine(username);
            var newPlayer = new PlayerProfile { Username = username };

            _dbContext.Add(newPlayer);
            await _dbContext.SaveChangesAsync();

            DatabaseLogger.LogDatabaseState(_dbContext);

            var playerDto = new PlayerProfileDto
            {
                Id = newPlayer.Id,
                Username = newPlayer.Username,
                Bio = newPlayer.Bio
            };

            return Ok(playerDto);
        }

        [HttpGet("{userId:guid}/stories")]
        public async Task<ActionResult> GetStories([FromRoute] Guid userId)
        {
            var stories = await _dbContext.StorySessions
                .Where(s => s.PlayerProfileId == userId)
                .Select(s => new StorySessionDto
                {
                    Prompt = s.Prompt,
                    Response = s.Response,
                    CreatedAt = s.CreatedAt
                }).ToListAsync();
            
            return Ok(stories);
        }
    }
}
