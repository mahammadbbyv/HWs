import { useEffect, useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'
import Save from './Save'

function App() {
  const [isLoading, setIsLoading] = useState(false);
  useEffect(() => {
  }, [])
  return (
    <>
    
    {isLoading ? <div>Loading...</div> : <Save setIsLoading={setIsLoading} />}
    </>
  )
}

export default App
