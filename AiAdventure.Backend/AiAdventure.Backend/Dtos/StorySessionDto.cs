﻿namespace AiAdventure.Backend.Dtos
{
    public class StorySessionDto
    {
        public string Prompt { get; set; }
        public string Response { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
