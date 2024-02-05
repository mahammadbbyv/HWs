import { useEffect } from "react"
import { useSelector, useDispatch } from "react-redux"
import { fetchContent } from "../redux/reducer"
import Dots from "react-activity/dist/Dots";
import "react-activity/dist/Dots.css";
import { Card, Col, Row } from 'antd';

function Home({query}) { 
  let usersArray = useSelector((state) => state.goods.goodsArray)
  let loading = useSelector((state) => state.goods.isLoading)
  let error = useSelector((state) => state.goods.error)
  let dispatch = useDispatch()
  useEffect(() => {
    dispatch(fetchContent())
  },[])
  if(loading){
    return <Dots />
  }
  return (
    <div >
      <h1>ALL GOODS</h1>
      <Row gutter={16}>
        {usersArray.map((item) => {
          return(
          <Col span={8}>
          <Card title={item.product_name} bordered={false}>
            <p>{item.product_description}</p>
            <p>{item.product_price}</p>
            <p>{item.store_name}</p>
            <p>{item.store_address}</p>
          </Card>
          </Col>
          )
        })}
        </Row>
    </div>
  )
}

export default Home
