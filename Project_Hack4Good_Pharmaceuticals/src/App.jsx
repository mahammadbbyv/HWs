import { useState } from 'react'
import './App.css'
import Header from './assets/Header'
import Main from './assets/Main'
import Footer from './assets/Footer'

function App() {
  const [count, setCount] = useState(0)
  
  return (
    <>
    <Header />
    <Main />
    <Footer />
    </>
  )
}

export default App
