import { useState, useEffect } from "react";

function GamePage() {

    const [prompt, setPrompt] = useState("");
    const [story, setStory] = useState("");
    const [history, setHistory] = useState([]);
    const [showSaveButtons, setShowSaveButtons] = useState(false);

    useEffect(() => {
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
            console.log("Fetched stories:", stories);
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
            body: JSON.stringify({playerId: localStorage.getItem("playerId"), prompt: prompt, response: story }),
        });
        console.log(response);
        setShowSaveButtons(false);
    }

    function handleDontSave() {
        setShowSaveButtons(false);
    }

    return (
        <>
            <h1>Ai Adventure Game</h1>
            <div>
                {story}
            </div>
            {showSaveButtons && (
                <div>
                    <p>Do you want to save this message?</p>
                    <button onClick={handleSave}>Yes</button>
                    <button onClick={handleDontSave}>No</button>
                </div>
            )}
            <div>
                <input type="text" id="input" />
                <button onClick={() => handleUserInput(document.getElementById('input').value)}>submit</button>
            </div>
        </>
    )
}

export default GamePage;