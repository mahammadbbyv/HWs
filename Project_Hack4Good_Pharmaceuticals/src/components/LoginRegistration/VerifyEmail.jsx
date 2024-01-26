import React, { useState } from 'react';
import OtpInput from 'react-otp-input';
import './style/Verify.css'

function Verify(){
    async function verifyEmail(email, code){
        try {
        fetch('https://api.ipify.org?format=json')
        .then(response => response.json())
        .then(data => setIPAddress(data.ip))
        .catch(error => console.log(error))
        console.log(ipAddress)
        const res = await fetch(`https://magab17-001-site1.ltempurl.com/verifyEmail?email=${email}&code=${code}`)
        const data = await res.json()
        console.log(data)
        if(data.ok){
            navigate('/login');
        }else{
            alert(data.message)
        }
        } catch (err) {
        console.log(err)
        }
    }
    const [otp, setOtp] = useState("");
    function handleChange(otp){
        setOtp(otp);
        if(otp.length === 6){
            
        }
    }
    return(
        <main>
            <div className='verify-box'>
                <h1 className="verify-title">Verify Email</h1>
                <p className='verify-content'>We've sent you an email with verification code. To verify your email please follow that link, then you may login.<br /> Otherwise, you won't be able to log in.</p>
            </div>
            <div className='verify-input'>
                <OtpInput
                    value={otp}
                    onChange={handleChange}
                    numInputs={6}
                    renderSeparator={<span>-</span>}
                    renderInput={(props) => <input {...props} />
                    
                }/>
            </div>
        </main>
    )
}

export default Verify