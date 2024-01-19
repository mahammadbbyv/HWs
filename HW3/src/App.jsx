import { useEffect, useState } from 'react'
import Users from './assets/Users'
import Posts from './assets/Posts'
import './App.css'

function App() {
  const [state, setState] = useState(false)
  const [value, setValue] = useState("")
  const [userId, setUserId] = useState("")
  const [filteredUserArray, setFilteredUserArray] = useState([])
  const [filteredPostArray, setFilteredPostArray] = useState([])
  return (
    <>
      {state ? null : <Users setState={setState} filteredUserArray={filteredUserArray} setFilteredUserArray={setFilteredUserArray} value={value} setValue={setValue} setUserId={setUserId}/>}
      {state ? <Posts setState={setState} filteredPostArray={filteredPostArray} setFilteredPostArray={setFilteredPostArray} userId={userId}/> : null}
    </>
  )
}

export default App
