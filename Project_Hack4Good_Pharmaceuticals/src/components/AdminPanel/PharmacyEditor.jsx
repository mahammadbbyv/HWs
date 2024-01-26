import './styles/PharmacyEditor.css'
import CustomButton from '../CutomButton.jsx'
import { useState } from 'react';

function PharmacyEditor(){
    const [name, setName] = useState('')
    const [phone, setPhone] = useState('')
    const [email, setEmail] = useState('')
    const [address, setAddress] = useState('')
    const [city, setCity] = useState('')
    function handleChange(val, regex, set, par){
        if(regex.test(val)){
            set(val)
            val = par
        }
    }
    return(
        <>
        <div className='pharmacy-editor-container'>
            <h3>Pharmacy Editor</h3>
            
            <div className='pharmacy-inputs-wrapper'>
                <div className='input-box'>
                    <input type='text' placeholder='Pharmacy Name...' value={name} onChange={(ev) => (handleChange(ev.target.value, /^[A-Za-z\s]*$/, setName, name))}/>
                    <input type='tel' placeholder='Phone Number...' value={phone} onChange={(ev) => (setPhone(ev.target.value))}/>
                    <input type='email' placeholder='E-Mail...' value={email} onChange={(ev) => (setEmail(ev.target.value))}/>
                </div>

                <div className='input-box'>
                    <input type='text' placeholder='Address' value={address} onChange={(ev) => (setAddress(ev.target.value))}/>
                    <input type='text' placeholder='City' value={city} onChange={(ev) => (handleChange(ev.target.value, /^[A-Za-z]+$/, setCity))}/>
                </div>
                
                <div className='submit-button'>
                    <CustomButton text={'Submit'}/>
                </div>
            </div>
        </div>
        </>
    )
}

export default PharmacyEditor