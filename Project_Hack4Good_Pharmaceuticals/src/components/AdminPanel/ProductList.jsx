import { useEffect, useState } from 'react'
import './styles/ProductList.css'
import reloadImg from '../../assets/reload-circular-arrow-symbol.png'
import axios from 'axios'

function ProductList({id, removeProduct}){
  const [products, setProducts] = useState([])
  const [reload, setReload] = useState(true)
  useEffect(() => {
      async function getData(){
          await axios.get(`https://magab17-001-site1.ltempurl.com/getPharmacyPharmaceuticals/${id}`)
          .then(res => {
            setProducts(res.data.res)
            return;
          })
        }
      getData();
  },[reload]);
    return(
      <>
      <div className='product-list-container'>
          <div className='product-list-header'>
            <h2>Products List</h2>
            <button onClick={() => setReload(!reload)}>
              <img src={reloadImg}/>
            </button>
          </div>
          {!products ? <h4>No products</h4> : products.map((item) => (
            <div className='product' key={item.Id}>
                  <h2 >{item.Name}</h2>
                  <button onClick={() => {setReload(!reload); removeProduct(item.Name)}}>Remove</button>
              </div>
          ))}
      </div>
      </>
  )
}

export default ProductList