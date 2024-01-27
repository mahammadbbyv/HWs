import './style/Login.css'
import CustomButton from '../CutomButton.jsx'
import { useEffect, useState } from 'react'
import passwordImage from '../../assets/password.png'
import { CookiesProvider, useCookies } from 'react-cookie'
import axios from 'axios'
import { Navigate, useNavigate } from 'react-router-dom';
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

function Login(){
    const [email, setEmail] = useState('')
    const navigate = useNavigate();
    const [ipAddress, setIPAddress] = useState('')
    const [password, setPassword] = useState('')
    const [cookies, setCookie] = useCookies(["token"]);
    useEffect(() => {
        try{
            axios.get(`https://magab17-001-site1.ltempurl.com/loginWithToken?token=${cookies.token}`)
            .then(res => {
                if(res.data.ok){
                    window.location.href = '/admin'
                }else{
                    console.log(res)
                }
            })
        }catch(err){
            console.log(err)
        }
    }
    , [])
    async function login(){
        try {
        fetch('https://api.ipify.org?format=json')
        .then(response => response.json())
        .then(data => setIPAddress(data.ip))
        .catch(error => console.log(error))
        const res = await fetch(`https://magab17-001-site1.ltempurl.com/login?email=${email}&password=${password}&ipaddress=${ipAddress}`)
        const data = await res.json()
        if(data.ok){
            toast.success('Successfully logged in!')
            setCookie('token', data.token)
            setTimeout(() => {
                window.location.href = '/admin'
            }, 3000)
        }else{
            alert(data.message)
        }
        } catch (err) {
        console.log(err)
        }
    }
    return(
        <>
        <main>
            <div className='login-wrapper'>
                <img src={passwordImage}/>
                <div className="login-container">
                    <div className="login">
                        <h2>Login</h2>
                        <input type='email' placeholder="E-Mail..." onChange={(val) => setEmail(val.target.value)}/>
                        <input type='password' placeholder="Password..." onChange={(val) => setPassword(val.target.value)}/>
                    </div>
                    <div className='login-buttons'>
                        <button className='btn' onClick={() =>login()}>Login</button>
                        <CustomButton text={"Register"} linkText={"register"} />
                    </div>
                </div>
            </div>
        </main>
        </>
    )
}

export default Login