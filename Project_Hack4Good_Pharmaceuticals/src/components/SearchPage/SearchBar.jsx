import { FaSearch } from "react-icons/fa";
import './styles/SearchBar.css'

function SearchBar({setSearch}){
    return (
        <div className="search-input-box">
            <FaSearch id="search-icon" />
            <input
                placeholder="Type to search..."
                onChange={(e) => setSearch(e.target.value)}
            />
        </div>
    )
}

export default SearchBar