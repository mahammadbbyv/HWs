import { useState } from "react"
import SearchPharmaceuticals from "./SearchPharmaceuticals"
import Pharmacies from "./Pharmacies"
import './styles/Search.css'

function Search(){
    const [product, setProduct] = useState('')
    return(
        <>
        <div className="search-pharms">
            <SearchPharmaceuticals setProduct={setProduct} className="pharmaceuticals search" />
            {product.length != 0 ? <Pharmacies product={product}/> : null}
        </div>
        </>
    )
}

export default Search