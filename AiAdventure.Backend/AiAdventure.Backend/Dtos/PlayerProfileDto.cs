namespace AiAdventure.Backend.Dtos;

public class PlayerProfileDto
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string? Bio { get; set; }
}