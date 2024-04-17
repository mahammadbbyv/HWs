import { useState } from 'react'
import ListeningTranslation from './Components/ListeningTranslation'
import Translate from './Components/Translate'
import './App.css'

function App() {
  const [text, setText] = useState("")
  const [lang, setLang] = useState("en")
  return (
    <>
    <div style={{display: "flex", width: "100%", justifyContent: "space-between"}}>
      <ListeningTranslation text={text} setText={setText} setLang={setLang} />
      <Translate sentence={text} language={lang}/>
    </div>
    </>
  )
}

export default App
