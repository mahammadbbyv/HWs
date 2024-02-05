import { useState } from "react"
import { useDispatch } from "react-redux"
import { editData } from "../redux/getUsersData"
import { Button, Input  } from 'antd';

function ModalWindowEdit({setShowModal, id}) {
    let [name, setName] = useState('')
    let [description, setDescription] = useState('')
    let [price, setPrice] = useState('')
    let [storeName, setStoreName] = useState('')
    let [storeAdress, setStoreAdress] = useState('')
    let dispatch = useDispatch()
    function Update(){
        dispatch(editData({
            id: id,
            product_name: name,
            product_description: description,
            product_price: price,
            store_name: storeName,
            store_address: storeAdress
        }))
        setShowModal(false)
    }
    return (
        <div className="modal-container">
        <div className="modal">
            <Button className="modalExit" onClick={() => setShowModal(false)}>X</Button>
            <h2>Edit Product</h2>
            <Input onChange={(ev) => setName(ev.target.value)} type="text" placeholder="Title"/>
            <Input onChange={(ev) => setDescription(ev.target.value)} type="text" placeholder="Description"/>
            <Input onChange={(ev) => setPrice(ev.target.value)} type="number" placeholder="Price"/>
            <Input onChange={(ev) => setStoreName(ev.target.value)} type="text" placeholder="Store Name"/>
            <Input onChange={(ev) => setStoreAdress(ev.target.value)} type="text" placeholder="Store Address"/>
            <Button onClick={() => Update()}>Edit</Button>
        </div>
        </div>
    )
}

export default ModalWindowEdit