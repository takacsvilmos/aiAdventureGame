namespace AiAdventure.Backend.Models;

public class PlayerProfile
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Username { get; set; }
    public string? Bio { get; set; }
    
    public List<StorySession> StorySessions { get; set; } = new();
}