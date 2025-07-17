import { useEffect, useState } from "react";
import { useNavigate }
 from "react-router-dom"

function HomePage() {
    const [username, setUsername] = useState("");
    const navigate = useNavigate();

    const handleStartGame = async () => {

        if (!username.trim()) return;

        const response = await fetch(`http://localhost:5177/api/User/${username}`, {
            method: 'POST',
        });

        const player = await response.json();

        if (player.id) {
            localStorage.setItem('playerId', player.id);
            localStorage.setItem('username', username);
            navigate('/game');
        } else {
            console.error(response);
        }



    }

    return (
        <div className="home-page-container">
            <h1 className="home-page-title">AI Adventure Game</h1>
            <input
                type="text"
                placeholder="Enter your name"
                value={username}
                onChange={(e) => setUsername(e.target.value)}
                className="home-page-input"
            />
            <button
                onClick={handleStartGame}
                className="home-page-button"
            >
                Start Game
            </button>
        </div>
    );
}

export default HomePage;