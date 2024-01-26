import { useState, useEffect } from "react"
import parse from 'html-react-parser';
import './styles/Pharmacies.css'

function Pharmacies({product}){
    const [results, setResults] = useState([])
    
    useEffect(() =>{
        const getData = async () => {
          try {
            if(product.length == 0) return setResults([])
            const res = await fetch(`https://magab17-001-site1.ltempurl.com/getPharmacies/Baku/${product}`)
            const data = await res.json()
            return setResults(data)
          } catch (err) {
            console.log(err)
          }
        }
        getData()
    }, [product])
    
    return(
        <div className="pharmacies-results">
            {results.map((item) => (
                <div className="pharmacy" key={item.id} >
                  <div className="pharmacy-info">
                    <div className="pharmacy-title">
                      <h2>{item.name}</h2>
                    </div>
                    <div className="pharmacy-contact">
                      <p>{item.email}</p>
                      <p>+{item.phoneNumber}</p>
                    </div>
                  </div>
                  {parse(item.address)}
                </div>
            ))}
        </div>
    )
}

export default Pharmacies