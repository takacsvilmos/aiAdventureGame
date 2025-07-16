using AiAdventure.Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace AiAdventure.Backend.Data;

public class AiAdventureDbContext : DbContext
{
    public AiAdventureDbContext(DbContextOptions<AiAdventureDbContext> options) : base(options) { }
    
    public DbSet<PlayerProfile> PlayerProfiles { get; set; }
    public DbSet<StorySession> StorySessions { get; set; }
    
}