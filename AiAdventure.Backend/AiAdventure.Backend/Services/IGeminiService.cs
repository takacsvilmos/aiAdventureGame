﻿using aiAdventureControllers;
public interface IGeminiService
{
    Task<string> GenerateContentAsync(string prompt);
}