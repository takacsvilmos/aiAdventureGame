
namespace AiAdventure.Backend.Dtos
{
    public class SaveStoryRequest
    {
        public Guid PlayerId { get; set; }
        public string Prompt { get; set; }
        public string Response { get; set; }
    }
}