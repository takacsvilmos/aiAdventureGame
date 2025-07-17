import { useState } from 'react'
import './App.css'
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom'
import HomePage from './HomePage'
import GamePage from './GamePage'

function App() {
  return (
    <div className="app-container">
      <Router>
        <Routes>
          <Route path="/" element={<HomePage />}></Route>
          <Route path="/game" element={<GamePage />}></Route>
        </Routes>
      </Router>
    </div>
  )
}

export default App
