import './styles/SearchResultsList.css'
import SearchResult from './SearchResult'

function SearchResultsList({results, setProduct}){
    return(
        <div className="results-list">
            {results.map((item) => (
                <SearchResult item={item} setProduct={setProduct} key={item.id}/>
            ))}
        </div>
    )
}

export default SearchResultsList