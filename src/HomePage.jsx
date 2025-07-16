import { useState } from "react";
import { useNavigate } from "react-router-dom"

function HomePage() {
    const [username, setUsername] = useState("");
    const navigate = useNavigate();

    const handleStartGame = async () => {
        if (!username.trim()) return;

        const response = await fetch("url", {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ username })
        });

        const player = await response.json();
        localStorage.setItem('playerId', player.id);
        navigate('/game');
    }

    return (
        <div className="min-h-screen flex flex-col items-center justify-center bg-gray-900 text-white">
            <h1 className="text-4xl font-bold mb-6">AI Adventure Game</h1>
            <input
                type="text"
                placeholder="Enter your name"
                value={username}
                onChange={(e) => setUsername(e.target.value)}
                className="p-2 rounded text-black mb-4"
            />
            <button
                onClick={handleStartGame}
                className="bg-blue-500 px-4 py-2 rounded hover:bg-blue-600 transition"
            >
                Start Game
            </button>
        </div>
    );
}

export default HomePage;