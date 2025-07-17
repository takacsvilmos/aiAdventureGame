import { useState, useEffect } from "react";

function GamePage() {

    const [prompt, setPrompt] = useState("");
    const [story, setStory] = useState("");
    const [history, setHistory] = useState([]);
    const [showSaveButtons, setShowSaveButtons] = useState(false);
    const [currentPlayer, setCurrentPlayer] = useState("");

    useEffect(() => {
        const storedUsername = localStorage.getItem('username');
        if (storedUsername) {
            setCurrentPlayer(storedUsername);
        }

        const fetchStories = async () => {
            try {
                const response = await fetch(`http://localhost:5177/api/User/${localStorage.getItem("playerId")}/stories`, {
                    method: 'GET',
                    headers: { 'Content-Type': 'application/json' },
                });

                if (!response.ok) {
                    const errorText = await response.text();
                    console.error("Failed to fetch stories:", errorText);
                    return;
                }

                const stories = await response.json();
                setHistory(stories);
            } catch (error) {
                console.error("Error during fetch:", error);
            }
        };

        fetchStories();
    }, [showSaveButtons]);

    async function handleUserInput(userInput) {
        const response = await fetch('http://localhost:5177/api/Ai/generate', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ prompt: userInput }),
        });

        const data = await response.json();
        const text = data.candidates[0].content.parts[0].text;
        setPrompt(userInput);
        setStory(text);
        setShowSaveButtons(true);
    }

    async function handleSave() {
        const response = await fetch('http://localhost:5177/api/Ai/save', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ playerId: localStorage.getItem("playerId"), prompt: prompt, response: story }),
        });
        setShowSaveButtons(false);
    }

    function handleDontSave() {
        setShowSaveButtons(false);
    }

    return (
        <div className="game-page-container">
            <div className="player-display">Welcome, {currentPlayer}!</div>
            <h1 className="game-title">AI Adventure Game</h1>
            <div className="game-content-wrapper">
                <div className="story-box">
                    {story || "Your story will appear here..."}
                </div>
                {showSaveButtons && (
                    <div className="save-buttons-container">
                        <p className="save-prompt">Do you want to save this message?</p>
                        <div className="save-buttons">
                            <button onClick={handleSave} className="save-button save-button-yes">Yes</button>
                            <button onClick={handleDontSave} className="save-button save-button-no">No</button>
                        </div>
                    </div>
                )}
                <div className="input-section">
                    <input type="text" id="input" className="game-input" placeholder="What do you do next?" />
                    <button onClick={() => handleUserInput(document.getElementById('input').value)} className="submit-button">Submit</button>
                </div>
                <div className="history-box">
                    <h2 className="history-title">History</h2>
                    <div className="history-list">
                        {history.map((item, index) => (
                            <div key={index} className="history-item slide-in">
                                <p className="history-item-prompt">{item.prompt}</p>
                                <p className="history-item-response">{item.response}</p>
                            </div>
                        ))}
                    </div>
                </div>
            </div>
        </div>
    )
}

export default GamePage;