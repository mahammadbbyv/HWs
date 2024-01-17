import { useState } from "react"

function Users({setShowModal, array}) {
  const [value, setValue] = useState('')
  
  return (
    <div>
        <button onClick={() => setShowModal(true)}>Add</button>
         <ul>
            {array.map((item) => {
            return(
                    <li>
                        <p>{item.name}</p>
                        <p>{item.username}</p>
                        <p>{item.email}</p>
                    </li>
            )
            })}
         </ul>
    </div>
  )
}

export default Users