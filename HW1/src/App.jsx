import { useState } from 'react'
import './App.css'
import First from './assets/First'
import Second from './assets/Second'
import Third from './assets/Third'

function App() {
  const [array, setArray] = useState([
    {
      title: "asd1",
      description: "asdasd1",
    },
    {
      title: "asd2",
      description: "asdasd2",
    },
    {
      title: "asd3",
      description: "asdasd3",
    },
  ])

  return (
    <>
      <First obj={array[0]}/>
      <Second obj={array[1]}/>
      <Third obj={array[2]}/>
    </>
  )
}

export default App
