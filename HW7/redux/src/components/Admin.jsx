import { useState, useEffect } from "react"
import { useDispatch, useSelector } from "react-redux"
import { fetchContent } from "../redux/reducer"
import { addData, deleteData, editData } from "../redux/getUsersData"
import Dots from "react-activity/dist/Dots";
import "react-activity/dist/Dots.css";
import ModalWindowEdit from "./ModalWindowEdit.jsx";
import { Button, Card, Col, Row } from 'antd';

function Admin() {
  let usersArray = useSelector((state) => state.goods.goodsArray)
  let loading = useSelector((state) => state.goods.isLoading)
  let error = useSelector((state) => state.goods.error)
  const [showModal, setShowModal] = useState(false)
  const [id, setId] = useState(0)
  let dispatch = useDispatch()
  useEffect(() => {
    dispatch(fetchContent())
  },[])
  if(loading){
    return (
      <div>
      <h1>ADMIN</h1>
        <Dots />
      </div>
      )
  }

  if(error){
    return (
      <div>
      <h1>ADMIN</h1>
        <p>ERROR</p>
      </div>
      )
  }

  return (
    <div>
      <h1>ADMIN</h1>
        <Button onClick={() => {
        dispatch(addData({
          product_name: item.product_name,
          product_description: item.product_description,
          product_price: item.product_price,
          store_name: item.store_name,
          store_address: item.store_address,
        }))
        console.log(usersArray)
      }}>Add Product</Button>
      <br/>
      <Button onClick={() => {
        for(let i = 0; i < 1000; i++){
          dispatch(addData({
            product_name: item.product_name,
            product_description: item.product_description,
            product_price: item.product_price,
            store_name: item.store_name,
            store_address: item.store_address,
          }))
        }
        console.log(usersArray)
      }}>add items to array</Button>
      <Row gutter={16}>
        {usersArray.map((item) => {
          return(
          <Col span={8}>
          <Card title={item.product_name} bordered={false}>
            <p>{item.product_description}</p>
            <p>{item.product_price}</p>
            <p>{item.store_name}</p>
            <p>{item.store_address}</p>
      <Button onClick={() => {
        dispatch(deleteData(item.id))
        console.log(usersArray)
      }}>➖</Button>
      <Button onClick={() => {
        setId(item.id)
        setShowModal(true)
      }}>🖊️</Button>
          </Card>
          </Col>
          )
        })}
        </Row>
        {showModal && <ModalWindowEdit setShowModal={setShowModal} id={id} />}
      
    </div>
  )
}

export default Admin
