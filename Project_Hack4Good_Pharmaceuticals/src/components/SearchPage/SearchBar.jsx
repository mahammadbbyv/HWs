import { FaSearch } from "react-icons/fa";
import { useState } from "react";
import './styles/SearchBar.css'

function SearchBar({setSearch, placeHolder}){
    let [holder, setPlaceHolder] = useState('Type to search...')

    if (placeHolder != null) {
        holder = placeHolder
    }
    return (
        <div className="search-input-box">
            <FaSearch id="search-icon" />
            <input
                placeholder={holder}
                onChange={(e) => setSearch(e.target.value)}
            />
        </div>
    )
}

export default SearchBar