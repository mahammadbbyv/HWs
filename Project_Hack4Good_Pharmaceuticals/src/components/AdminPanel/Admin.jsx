import './styles/Admin.css'
import PharmacyEditor from './PharmacyEditor.jsx'
import ProductEditor from './ProductEditor.jsx'
import ProductList from './ProductList.jsx'
import { useEffect, useState } from 'react'
import { CookiesProvider, useCookies } from 'react-cookie'
import axios from 'axios'

function Admin(){
    const [product, setProduct] = useState([])
    const [name, setName] = useState('')
    const [address, setAddress] = useState('')
    const [phone, setPhone] = useState('')
    const [email, setEmail] = useState('')
    const [city, setCity] = useState('')
    const [cookies, setCookie] = useCookies(["token"]);
    useEffect(() => {
        if(cookies.token){
            axios.get(`https://magab17-001-site1.ltempurl.com/verifyToken?token=${cookies.token}`)
            .then(res => {
                if(res.data.ok){
                    console.log(res)
                }
            })
        }
    });
    return(
        <>
        <main>
            <div className='admin-container'>
                <div className='admin-editors'>
                    <PharmacyEditor />
                    <ProductEditor />
                </div>
                <div className='admin-list'>
                    <ProductList product={product}/>
                </div>
            </div>
        </main>
        </>
    )
}

export default Admin