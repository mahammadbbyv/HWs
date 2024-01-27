import React, { useState } from 'react';
import OtpInput from 'react-otp-input';
import './style/Verify.css' //cookie import
import { CookiesProvider, useCookies } from 'react-cookie'

function Verify(){
    const [cookies, setCookie] = useCookies(["email"]);
    async function verifyEmail(code){
        try {
            console.log(cookies.email)
            const res = await fetch(`https://magab17-001-site1.ltempurl.com/verifyEmail?email=${cookies.email}&code=${code}`)
            const data = await res.json()
            if(data.ok){
                window.location.href = '/login'
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
            verifyEmail(otp)
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