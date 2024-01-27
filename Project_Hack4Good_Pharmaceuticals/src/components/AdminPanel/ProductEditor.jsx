import {useState, useEffect} from 'react'
import './styles/ProductEditor.css'
import SearchBar from '../SearchPage/SearchBar'
import SearchResultsList from '../SearchPage/SearchResultsList'

function ProductEditor({setProduct}){
    const [search, setSearch] = useState('')
    const [results, setResults] = useState([])
    
    useEffect(() =>{
        const getData = async () => {
          try {
            if(search.length == 0) return setResults([])
            const res = await fetch(`https://magab17-001-site1.ltempurl.com/getProducts/${search}`)
            const data = await res.json()
            return setResults(data)
          } catch (err) {
            console.log(err)
          }
        }
        getData()
    }, [search])

    return(
        <>
        <div className='product-editor-container'>
            <h2>Product Addition</h2>
            <div className='product-editor'>
                <SearchBar setSearch={setSearch}/>
                {results && results.length > 0 && <SearchResultsList results={results} setProduct={setProduct} />}
            </div>
        </div>  
        </>
    )
}

export default ProductEditor