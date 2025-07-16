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
        <div className="flex h-screen bg-gray-800 text-white">
            <div className="w-2/3 p-8 flex flex-col">
                <h1 className="text-4xl font-bold mb-4">AI Adventure Game</h1>
                <div className="bg-gray-700 p-4 rounded-lg flex-grow overflow-y-auto">
                    {story || "Your story will appear here..."}
                </div>
                {showSaveButtons && (
                    <div className="mt-4 p-4 bg-gray-700 rounded-lg">
                        <p className="text-lg">Do you want to save this message?</p>
                        <div className="flex justify-end mt-2">
                            <button onClick={handleSave} className="bg-green-500 hover:bg-green-700 text-white font-bold py-2 px-4 rounded mr-2">Yes</button>
                            <button onClick={handleDontSave} className="bg-red-500 hover:bg-red-700 text-white font-bold py-2 px-4 rounded">No</button>
                        </div>
                    </div>
                )}
                <div className="mt-4">
                    <input type="text" id="input" className="bg-gray-700 text-white w-full p-2 rounded-lg" placeholder="What do you do next?" />
                    <button onClick={() => handleUserInput(document.getElementById('input').value)} className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded mt-2 w-full">Submit</button>
                </div>
            </div>
            <div className="w-1/3 p-8 bg-gray-900 overflow-y-auto">
                <h2 className="text-2xl font-bold mb-4">History</h2>
                <div className="space-y-4">
                    {history.map((item, index) => (
                        <div key={index} className="bg-gray-800 p-4 rounded-lg slide-in">
                            <p className="font-bold">{item.prompt}</p>
                            <p>{item.response}</p>
                        </div>
                    ))}
                </div>
            </div>
        </div>
    )
}

export default GamePage;
