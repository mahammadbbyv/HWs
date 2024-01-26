import './style/Register.css'
import CustomButton from '../CutomButton.jsx'
import { useState } from 'react'
import passwordImage from '../../assets/password.png'
import axios from 'axios'
import { Navigate, useNavigate } from 'react-router-dom';
import 'react-phone-number-input/style.css'
import PhoneInput from 'react-phone-number-input'
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

function Register(){
    const [email, setEmail] = useState('')
    const [phoneNumber, setPhoneNumber] = useState('')
    const [password, setPassword] = useState('')
    const [confirmPassword, setConfirmPassword] = useState('')
    async function register(){
        if(password === confirmPassword){
            // use fetch with params in query
            
            setPhoneNumber(phoneNumber.replace('+', ''));
            const res = await fetch(`https://magab17-001-site1.ltempurl.com/register?email=${email}&password=${password}&phonenumber=${phoneNumber}&confirmPassword=${confirmPassword}`)
            console.log(res)
            const data = await res.json()
            console.log(data)
            if(data.ok){
                toast.success('Successfully registered!')
                setTimeout(() => {
                    window.location.href = '/verifyEmail'
                }, 3000)
            }else{
                toast.error(data.res)
            }
        }else{
            toast.error('Passwords do not match!')
        }
        
    }
    return(
        <>
        <main>
            <div className='register-wrapper'>
                <img className='password-img' src={passwordImage}/>
                <div className="register-container">
                    <div className="register">
                        <h2>Register</h2>
                        <input placeholder="E-Mail..." onChange={(val) => setEmail(val.target.value)}/>
                        <PhoneInput
                            placeholder="Enter phone number..."
                            value={phoneNumber}
                            
                            onChange={setPhoneNumber}/>
                        <input type='password' placeholder="Password..." onChange={(val) => setPassword(val.target.value)}/>
                        <input type='password' placeholder="Confirm Password..." onChange={(val) => setConfirmPassword(val.target.value)} />
                    </div>
                    <div className='register-buttons'>
                        <CustomButton text={"Login"} linkText={"login"} />
                        <button className='btn' onClick={() => register()}>Register</button>
                    </div>
                </div>
            </div>
            <ToastContainer />
        </main>
        
        </>
    )
}

export default Register