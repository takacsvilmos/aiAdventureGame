# 🧩 Tech Stack

### 🖥️ Frontend

Built with modern tooling for speed and flexibility:

<div align="left"> <img src="https://img.shields.io/badge/Vite-646CFF?style=for-the-badge&logo=vite&logoColor=white" alt="Vite" /> <img src="https://img.shields.io/badge/React-61DAFB?style=for-the-badge&logo=react&logoColor=white" alt="React" /> <img src="https://img.shields.io/badge/JavaScript-F7DF1E?style=for-the-badge&logo=javascript&logoColor=black" alt="JavaScript" /> 
  <p>
  cd frontend<br>
  npm install<br>
  npm run dev<br>
  </p>

By default, it runs at http://localhost:5173 and connects to the backend at https://localhost:5001.
  
### 🔧 Backend

Robust API server built with .NET Core and integrates with Google Gemini for AI content generation:

<div align="left"> <img src="https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt=".NET" /> <img src="https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white" alt="C#" /> <img src="https://img.shields.io/badge/ASP.NET%20Core-5C2D91?style=for-the-badge&logo=dotnet&logoColor=white" alt="ASP.NET Core" /> <img src="https://img.shields.io/badge/REST%20API-6DB33F?style=for-the-badge&logo=postman&logoColor=white" alt="REST API" /> <img src="https://img.shields.io/badge/Gemini%20API-4285F4?style=for-the-badge&logo=google&logoColor=white" alt="Gemini API" /> </div> 

### 🧠 How It Works

This project uses Google Gemini API to generate text-based RPG responses.

    The user enters a prompt (e.g. "Explore the cave")

    The backend sends the prompt to Gemini with a “game master” system instruction

    Gemini returns a short narrative update (e.g. “You find a glowing sword…”)

    The frontend displays it in a game-like chat interface

🎮 Game Concept

    AI Adventure is a minimalist narrative engine powered by LLMs. Inspired by classic text adventures, but with endless AI-generated possibilities.

    🗨️ Simple prompts, rich stories

    🎲 Roleplay mechanics powered by Gemini

🛡️ Environment & Security

    Secrets like API keys are stored in appsettings.Development.json (gitignored)

    CORS enabled for local frontend (localhost:5173)
    

✨ Future Ideas

    User character profiles

    Save/load game state

    Gemini Vision integration (for AI-generated images)

🤝 Credits

Created by Takács Vilmos
My Backlog: https://github.com/users/takacsvilmos/projects/5/views/1
