import express from 'express';
import fetch from 'node-fetch';
import cors from 'cors';
import dotenv from 'dotenv';

dotenv.config();
const app = express();
const PORT = 4000;

app.use(cors());
app.use(express.json());

const GEMINI_API_KEY = process.env.VITE_GEMINI_API_KEY;
const MODEL_NAME = 'models/gemini-2.5-pro';

app.post('/api/generate', async (req, res) => {
  const prompt = req.body.prompt;
  const basePrompt = ", you are a gamemaster in a role playing game and make answers accordingly."
  try {
    const response = await fetch(`https://generativelanguage.googleapis.com/v1/models/gemini-1.5-pro:generateContent?key=${GEMINI_API_KEY}`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({
        contents: [
          {
            parts: [
              { text: prompt + basePrompt}
            ]
          }
        ]
      })
    });

    const data = await response.json();
    res.json(data);
  } catch (err) {
    console.error('Error from Gemini API:', err);
    res.status(500).json({ error: 'Failed to fetch from Gemini API' });
  }
});


app.listen(PORT, () => {
  console.log(`Server running at http://localhost:${PORT}`);
});