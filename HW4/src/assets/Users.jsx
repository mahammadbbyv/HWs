import { useState } from "react"

function Users({users, setState, setUser}){
    return(
        <>
        <div className='box'>
            {users.map((item) => {
            return(
            <div key={item.id}>
                <img src={item.photoUrl} alt={item.name}/>
                <p>{item.name}</p>
                <p>{item.username}</p>
                <p>{item.email}</p>
                <button onClick={() => {
                    setState(true)
                    setUser(item)
                }}>View</button>
            </div>
            )
            })}
        </div>
        </>
    )
}

export default Users