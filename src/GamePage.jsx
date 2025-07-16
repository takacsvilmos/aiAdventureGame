import { useEffect, useState } from "react";

function GamePage() {
    
    const [story, setStory] = useState("")

    async function handleUserInput(userInput) {
        const response = await fetch('http://localhost:5177/api/Ai/generate', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ prompt: userInput }),
        });

        const data = await response.json();
        console.log(data);
        const text = data.candidates[0].content.parts[0].text;
        setStory(text);
    }

    return (
        <>
            <h1>Ai Adventure Game</h1>
            <div>
                {story}
            </div>
            <div>
                <input type="text" id="input" />
                <button onClick={() => handleUserInput(document.getElementById('input').value)}>submit</button>
            </div>
        </>
    )
}

export default GamePage;