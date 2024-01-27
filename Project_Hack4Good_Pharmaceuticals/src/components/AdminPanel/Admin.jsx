import './styles/Admin.css'
import PharmacyEditor from './PharmacyEditor.jsx'
import ProductEditor from './ProductEditor.jsx'
import ProductList from './ProductList.jsx'
import { useEffect, useState } from 'react'
import { CookiesProvider, useCookies } from 'react-cookie'
import axios from 'axios'
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import ModalWindow from './ModalWindow.jsx'

function Admin(){
    const [id, setId] = useState(0)
    const [showModal, setShowModal] = useState(0)
    const [product, setProduct] = useState('')
    const [name, setName] = useState('')
    const [address, setAddress] = useState('')
    const [phone, setPhone] = useState('')
    const [email, setEmail] = useState('')
    const [city, setCity] = useState('')
    const [isVerified, setIsVerified] = useState(false)
    const [cookies, setCookie] = useCookies(["token"]);
    async function handleAdd(product){
        await axios.get(`https://magab17-001-site1.ltempurl.com/AddPharmaceuticalToPharmacy?name=${product}&pharmacyId=${id}&token=${cookies.token}`)
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
    async function handleRemove(product){
        await axios.get(`https://magab17-001-site1.ltempurl.com/RemovePharmaceuticalFromPharmacy?name=${product}&pharmacyId=${id}&token=${cookies.token}`)
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
    useEffect(() => {
        async function getData(){
            if(cookies.token != undefined && cookies.token.length > 0){
                await axios.get(`https://magab17-001-site1.ltempurl.com/getPharmacy?token=${cookies.token}`)
                .then(res => {
                    if(res.data.ok){
                        setId(res.data.res.Id)
                        setName(res.data.res.Name)
                        setAddress(res.data.res.Address)
                        setPhone(`${res.data.res.PhoneNumber}`)
                        setEmail(res.data.res.Email)
                        setCity(res.data.res.City)
                        setIsVerified(res.data.res.IsVerified)
                        return;
                    }else{
                        if(res.data.res == 'Token expired'){
                            setCookie('token', '')
                            window.location.href = '/login'
                            return;
                        }
                    }
                })
            }else{
                window.location.href = '/login'
                return;
            }
        }
        getData();
    },[handleAdd, handleRemove]);
    return(
        <>
        <main>
            <div className='admin-container'>
                <div className='admin-editors'>
                    <PharmacyEditor setShowModal={setShowModal} isVerified={isVerified} id={id} phone={phone} email={email} setPhone={setPhone} setEmail={setEmail} />
                    <ProductEditor setProduct={handleAdd} />
                </div>
                <div className='admin-list'>
                    <ProductList id={id} removeProduct={handleRemove}/>
                </div>
            </div>
            {showModal == 0 ? null : <ModalWindow setShowModal={setShowModal} showModal={showModal} />}
        </main>
        </>
    )
}

export default Admin