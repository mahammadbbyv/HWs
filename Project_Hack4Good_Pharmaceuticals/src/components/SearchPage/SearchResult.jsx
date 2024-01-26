import './styles/SearchResult.css'

function SearchResult({item, setProduct}){
    return(
        <>
        <a className="search-result" onClick={() => setProduct(item.name)}>
            <p>{item.name}</p>
        </a>   
        </>
    )
}

export default SearchResult