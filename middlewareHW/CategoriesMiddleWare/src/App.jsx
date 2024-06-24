import { useState } from 'react'
import './App.css'

function App() {
  const [department, setDepartment] = useState('');
  const [position, setPosition] = useState('');
  const [users, setUsers] = useState([]);

  const handleSubmit = (e) => {
    e.preventDefault();
    fetch(`http://localhost:3000/users?department=${department}&position=${position}`)
      .then(response => response.json())
      .then(data => setUsers(data))
      .catch(error => console.error(error));
  }

  return (
    <>
    <div>
      <form>
        <input type="text" placeholder="Department" value={department} onChange={(e) => setDepartment(e.target.value)} />
        <input type="text" placeholder="Position" value={position} onChange={(e) => setPosition(e.target.value)} />
        <button onClick={handleSubmit}>Submit</button>
      </form>
      <ul>
        {users.map((user, index) => <li key={index}>{user.name} - {user.department} - {user.position}</li>)}
      </ul>
    </div>
    </>
  )
}

export default App
