import { useState, useEffect } from "react"
import SearchBar from "./SearchBar"
import SearchResultsList from "./SearchResultsList"
import './styles/SearchPharmaceuticals.css'
import { motion } from "framer-motion"

function SearchPharmaceuticals({setProduct}){
    const [search, setSearch] = useState('')
    const [results, setResults] = useState([])
    
    useEffect(() =>{
        const getData = async () => {
          try {
            if(search.length == 0) return setResults([])
            const res = await fetch(`https://magab17-001-site1.ltempurl.com/getProducts/${search}`)
            const data = await res.json()
            console.log(data)
            return setResults(data)
          } catch (err) {
            console.log(err)
          }
        }
        getData()
    }, [search])

    return(
      <div className="search-container">
          <SearchBar setSearch={setSearch} />
          <div className="search-results">
            {results && results.length > 0 && <SearchResultsList results={results} setProduct={setProduct} />}
          </div>
      </div>
    )
}

export default SearchPharmaceuticals