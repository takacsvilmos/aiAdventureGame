using AiAdventure.Backend.Data;
using Microsoft.AspNetCore.Mvc;
using AiAdventure.Backend.Dtos;
using AiAdventure.Backend.Models;

namespace aiAdventureControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AiController : ControllerBase
    {
        private readonly IGeminiService _geminiService;
        private readonly AiAdventureDbContext _dbContext;

        public AiController(IGeminiService geminiService, AiAdventureDbContext dbContext)
        {
            _geminiService = geminiService;
            _dbContext = dbContext;
        }

        [HttpPost("generate")]
        public async Task<ActionResult> Generate([FromBody] PromptRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Prompt))
            {
                return BadRequest("Prompt cannot be empty.");
            }

            try
            {
                var result = await _geminiService.GenerateContentAsync(request.Prompt);

                if (string.IsNullOrWhiteSpace(result))
                {
                    return NotFound("Could not generate content based on the prompt.");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An internal error occurred.");
            }
        }

        [HttpPost("save")]
        public async Task<ActionResult> Save([FromBody] SaveStoryRequest saveStoryRequest)
        {
            var newStorySession = new StorySession
            {
                Prompt = saveStoryRequest.Prompt,
                Response = saveStoryRequest.Response,
                PlayerProfileId = saveStoryRequest.PlayerId
            };

            _dbContext.StorySessions.Add(newStorySession);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}