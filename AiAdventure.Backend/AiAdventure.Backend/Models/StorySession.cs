namespace AiAdventure.Backend.Models;

public class StorySession
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Prompt { get; set; }
    public string Response { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Foreign key
    public Guid PlayerProfileId { get; set; }
    public PlayerProfile PlayerProfile { get; set; }
}