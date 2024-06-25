/* eslint-disable no-unused-vars */
import { Routes, Route } from 'react-router-dom'
import './App.css'
import Forms from './Components/Forms'
import LoggedInPage from './Components/LoggedInPage'
import { useState } from 'react'
import { useEffect } from 'react'

function App() {
  return (
    <>
      <div className="App">
        <Routes>
          <Route path="/" element={<Forms />} />
          <Route path="/logged" element={<LoggedInPage />} />
        </Routes>
      </div>
    </>
  )
}

export default App
