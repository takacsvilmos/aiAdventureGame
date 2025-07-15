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
            var result = await _geminiService.GenerateContentAsync(request.Prompt);
            return Ok(result);
        }
    }
}