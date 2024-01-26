import './styles/ProductList.css'
import { useEffect, useState } from 'react'

function ProductList({product}){
    const [products, setProducts] = useState([])

    useEffect(() =>{
        const getData = async () => {
          try {
            if(product.length == 0) return setResults([])
            const res = await fetch(`https://magab17-001-site1.ltempurl.com/getPharmacies/Baku/${product}`)
            const data = await res.json()
            return setProducts(data)
          } catch (err) {
            console.log(err)
          }
        }
        getData()
    }, [product])
    
    return(
        <>
        <div className='product-list-container'>
            <h2>Products List</h2>
            {products.map((item) => (
                <div className='product'>
                    <h4>{item.name}</h4>
                </div>
            ))}
        </div>
        </>
    )
}

export default ProductList