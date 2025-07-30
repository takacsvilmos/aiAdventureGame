using System.Text;
using System.Text.Json;
using AiAdventure.Backend.Exceptions;

public class GeminiService : IGeminiService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public GeminiService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _apiKey = configuration["Gemini:ApiKey"];
    }

    public async Task<string> GenerateContentAsync(string prompt)
    {
        
        var basePrompt =
            ", you are a game master in a role playing game and make answers accordingly. respond in max 3 sentences.";
        var requestBody = new
        {
            contents = new[]
            {
                new
                {
                    parts = new[]
                    {
                        new { text = prompt + basePrompt }
                    }
                }
            }
        };

        var request = new HttpRequestMessage(HttpMethod.Post,
            $"https://generativelanguage.googleapis.com/v1/models/gemini-2.5-flash:generateContent?key={_apiKey}")
        {
            Content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json")
        };

        var response = await _httpClient.SendAsync(request);
        
        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new GeminiApiException("Something went wrong with the AI service.", response.StatusCode, errorContent);
        }

        var responseContent = await response.Content.ReadAsStringAsync();
        return responseContent;
    }
}