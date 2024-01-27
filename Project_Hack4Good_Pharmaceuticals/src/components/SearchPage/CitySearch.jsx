import SearchBar from "./SearchBar"
import SearchResultsList from "./SearchResultsList"
import { useEffect } from "react"
import './styles/CitySearch.css'

function CitySearch({city, setCity, resultsCity, setSearch, setResultsCity, search}){
    useEffect(() =>{
        const getData = async () => {
          try {
            if(search.length == 0) return setResultsCity([]) // with X-Api-Key in headers
            const res = await fetch(`https://api.api-ninjas.com/v1/city?name=${search}`, {
                method: 'GET',
                headers: {
                    'X-Api-Key': 'YyCNiWSniuPnDLqZrRD9Pw==LjdmQ2g9xyJ4Epm9'
                }
            })
            const data = await res.json()
            console.log(data);
            return setResultsCity(data)
          } catch (err) {
            console.log(err)
          }
        }
        getData()
    }, [search]);
    function handleCityChange(city){
        setCity(city)
        setSearch('')
    }
    return(
        <>
        <div className="city-container">
            <div className="city-search">
                <SearchBar setSearch={setSearch} placeHolder={'Search for City...'}/>
                {resultsCity && resultsCity.length > 0 && <SearchResultsList results={resultsCity} setProduct={handleCityChange} />}
            </div>
            <input className="city-result" type='text' placeholder='City' disabled value={`Your selected city is ${city}`} onChange={(ev) => (setCity(ev.target.value))}/>
        </div>       
        </>
    )
}

export default CitySearch