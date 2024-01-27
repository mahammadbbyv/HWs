import './styles/PharmacyEditor.css'
import CustomButton from '../CutomButton.jsx'
import { useEffect, useState } from 'react';
import 'react-phone-number-input/style.css'
import PhoneInput from 'react-phone-number-input'
import SearchBar from '../SearchPage/SearchBar.jsx'
import SearchResultsList from '../SearchPage/SearchResultsList.jsx';
import axios from 'axios';
import { CookiesProvider, useCookies } from 'react-cookie'
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

function PharmacyEditor({setShowModal, isVerified, id, phone, email, setPhone, setEmail}){
    const [search, setSearch] = useState('')
    const [results, setResults] = useState([])
    const [product, setProduct] = useState('')
    const [cookies, setCookie] = useCookies(["token"]);
    const [name, setName] = useState('')
    const [address, setAddress] = useState('')
    const [city, setCity] = useState('')
    useEffect(() =>{
        const getData = async () => {
          try {
            if(search.length == 0) return setResults([]) // with X-Api-Key in headers
            const res = await fetch(`https://api.api-ninjas.com/v1/city?name=${search}`, {
                method: 'GET',
                headers: {
                    'X-Api-Key': 'YyCNiWSniuPnDLqZrRD9Pw==LjdmQ2g9xyJ4Epm9'
                }
            })
            const data = await res.json()
            console.log(data);
            return setResults(data)
          } catch (err) {
            console.log(err)
          }
        }
        getData()
    }, [search]);

    
    async function submit(){
        console.log(`https://magab17-001-site1.ltempurl.com/updatePharmacy?pharmacyId=${id}&name=${name}&phoneNumber=${phone.replace('+ ', '')}&address=${encodeURIComponent(address)}&city=${product}&token=${cookies.token}`)
        await axios.get(`https://magab17-001-site1.ltempurl.com/updatePharmacy?pharmacyId=${id}&name=${name}&phoneNumber=${phone.replace('+ ', '')}&address=${encodeURIComponent(address)}&city=${product}&token=${cookies.token}`)
        .then(res => {
        if(res.data.ok){
            toast.success("Successfully added product!")
            return;
        }else{
            if(res.data.res == 'Token expired'){
                toast.warning(res.data.res)
                setCookie('token', '')
                setTimeout(() => {
                }, 3000)
                window.location.href = '/login'
                return;
            }
        }})
    }
    return(
        <>
        <div className='pharmacy-editor-container'>
            <h3>Pharmacy Editor ID:{id}</h3>
            
            <div className='pharmacy-inputs-wrapper'>
                <div className='input-box'>
                    <input type='text' placeholder='Pharmacy Name...' onChange={(ev) => {
                        setName(ev.target.value);
                        setTimeout(() => {
                            console.log(name);
                        }, 1000);
                    }}/>
                    <PhoneInput
                            placeholder="Enter phone number..."
                            value={phone}
                            onChange={setPhone}/>
                    <input type='email' placeholder='E-Mail...' value={email} onChange={(ev) => (setEmail(ev.target.value))}/>
                </div>

                <div className='location-inputs'>
                    <div className='input-box'>
                        <div className='address'>
                            <input type='text' placeholder='<iframe...' onChange={(ev) => (setAddress(ev.target.value))}/> <button onClick={() => setShowModal(1)}><h2>?</h2></button>
                        </div>
                        <input type='text' placeholder='City' disabled value={product} onChange={(ev) => (setCity(ev.target.value))}/>
                    </div> 
                    <div className='city-input'>
                        <SearchBar setSearch={setSearch} placeHolder={'Search for City...'}/>
                        {results && results.length > 0 && <SearchResultsList results={results} setProduct={setProduct} />}
                    </div>
                </div>
                <div className='last-column'>
                    <h4>Is Verified: <span>{isVerified ? "true" : "false"}</span></h4> <button onClick={() => setShowModal(2)}><h2>?</h2></button>
                    <div className='submit-button'>
                        <button className="btn" onClick={()=>submit()}>Submit</button>
                    </div>
                </div>
            </div>
        </div>
        </>
    )
}

export default PharmacyEditor