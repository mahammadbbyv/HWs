import { useState } from "react"
import { validEmail, validName, validUsername } from "./Regex";
function ModalWindow({setShowModal, setArray, array}) {
    let obj = {
        "id": String(Number(array.at(-1).id) + 1),
        "name": "",
        "username": "",
        "email": ""
    }
    function Regex(){
        if(validName.test(obj.Name)){
            if(validUsername.test(obj.username)){
                if(validEmail.test(obj.email)){
                    setArray([...array, obj]); setShowModal(false);
                }else{
                    alert("E-Mail is not valid")
                }
            }else{
                alert("Username is not valid")
            }
        }else{
            alert("Name is not valid")
        }
    }
    return (
        <div className="modal-container">
        <div className="modal">
            <button className="modalExit" onClick={() => setShowModal(false)}>X</button>
            <h2>Add User</h2>
            <input onChange={(val) => obj.name = val.target.value} type="text" placeholder="Name"/>
            <input onChange={(val) => obj.username = val.target.value} type="username" placeholder="Username"/>
            <input onChange={(val) => obj.email = val.target.value} type="email" placeholder="E-Mail"/>
            <button onClick={() => Regex()}>Add</button>
        </div>
        </div>
    )
}

export default ModalWindow