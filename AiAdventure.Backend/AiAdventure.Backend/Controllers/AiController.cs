using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace aiAdventureControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AiController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public AiController(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public class PromtRequest
        {
            public string Prompt { get; set; }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] PromtRequest request)
        {
            var apiKey = _configuration["Gemini:ApiKey"];
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                return StatusCode(500, new { error = "API Key is missing." });
            }

            var fullPrompt = request.Prompt +
                            ", you are a gamemaster in a role playing game and make answers accordingly. respond in max 3 sentences.";
            var payload = new
            {
                contents = new[]
                {
                    new
                    {
                        parts = new[]
                        {
                            new { text = fullPrompt }
                        }
                    }
                }
            };
            var json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var url = $"https://generativelanguage.googleapis.com/v1/models/gemini-2.5-flash:generateContent?key={apiKey}";

            try
            {
                var response = await _httpClient.PostAsync(url, content);
                var responseText = await response.Content.ReadAsStringAsync();

                return Content(responseText, "application/json");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error from Gemini API: " + ex.Message);
                return StatusCode(500, new { error = "Failed to fetch from Gemini API" });
            }
        }
    }
}