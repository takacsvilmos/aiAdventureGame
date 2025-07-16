using Microsoft.AspNetCore.Mvc;
using AiAdventure.Backend.Dtos;

namespace aiAdventureControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AiController : ControllerBase
    {
        private readonly IGeminiService _geminiService;

        public AiController(IGeminiService geminiService)
        {
            _geminiService = geminiService;
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
    }
}