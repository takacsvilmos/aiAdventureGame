using AiAdventure.Backend.Data;
using System.Text.Json;

namespace AiAdventure.Backend.Services
{
    public static class DatabaseLogger
    {
        public static void LogDatabaseState(AiAdventureDbContext context)
        {
            Console.WriteLine("--- Logging Database State ---");

            var playerProfiles = context.PlayerProfiles.ToList();
            Console.WriteLine($"Player Profiles: {playerProfiles.Count}");
            Console.WriteLine(JsonSerializer.Serialize(playerProfiles, new JsonSerializerOptions { WriteIndented = true }));

            var storySessions = context.StorySessions.ToList();
            Console.WriteLine($"Story Sessions: {storySessions.Count}");
            Console.WriteLine(JsonSerializer.Serialize(storySessions, new JsonSerializerOptions { WriteIndented = true }));

            Console.WriteLine("--- End Logging Database State ---");
        }
    }
}
