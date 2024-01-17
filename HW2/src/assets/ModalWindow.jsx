import { useState } from "react"

function ModalWindow({setShowModal, setArray, array}) {
    let obj = {
        "name": "",
        "username": "",
        "email": ""
    }
    return (
        <div className="modal-container">
        <div className="modal">
            <button className="modalExit" onClick={() => setShowModal(false)}>X</button>
            <h2>Add User</h2>
            <input onChange={(val) => obj.name = val.target.value} type="text" placeholder="Name"/>
            <input onChange={(val) => obj.username = val.target.value} type="username" placeholder="Username"/>
            <input onChange={(val) => obj.email = val.target.value} type="email" placeholder="E-Mail"/>
            <button onClick={() => setArray([...array, obj])}>Add</button>
        </div>
        </div>
    )
}

export default ModalWindow