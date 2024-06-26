import { useState } from 'react'
import './App.css'

function App() {
  const [texts, setTexts] = useState({
    text1: '',
    text2: '',
    text3: '',
    text4: '',
    text5: ''
  });
  const [result, setResult] = useState('')

  const create = async () => {
    const res = await fetch(`http://localhost:3000/textsToFile?text1=${texts.text1}&text2=${texts.text2}&text3=${texts.text3}&text4=${texts.text4}&text5=${texts.text5}`,
      {
        method: 'POST'
      }
    )
    const data = await res.text()
    setResult(data)
  }

  return (
    <>
    <div>
      <h1>Multiple Texts to One String to File</h1>
      <textarea id="text1" onChange={(e) => setTexts({...texts, text1: e.target.value})}></textarea>
      <textarea id="text2" onChange={(e) => setTexts({...texts, text2: e.target.value})}></textarea>
      <textarea id="text3" onChange={(e) => setTexts({...texts, text3: e.target.value})}></textarea>
      <textarea id="text4" onChange={(e) => setTexts({...texts, text4: e.target.value})}></textarea>
      <textarea id="text5" onChange={(e) => setTexts({...texts, text5: e.target.value})}></textarea>
      <button onClick={() => create()}>Create</button>
      <h1>Result</h1>
      <p>{result}</p>
    </div>
    </>
  )
}

export default App
