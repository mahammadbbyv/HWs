/* eslint-disable react/prop-types */
/* eslint-disable no-unused-vars */
import { useCookies } from "react-cookie";
import { useState, useEffect } from 'react'
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

function Forms() {
    const [name, setName] = useState('')
    const [email, setEmail] = useState('')
    const [cookies, setCookies] = useCookies()
    const [password, setPassword] = useState('')

    useEffect(() => {
        if (cookies.passwordHashed) {
            fetch(`http://localhost:3000/verify?password=${cookies.passwordHashed}`)
            .then(response => response.json())
            .then(data => {
                if (data.ok) {
                    toast(data.message)
                    setTimeout(() => {
                        window.location.href = '/logged'
                    }, 2000)
                } else {
                    toast(data.message)
                }
            })
            .catch(error => console.error(error));
        }
    }, [cookies.passwordHashed])
  
    const handleRegister = async (e) => {
      e.preventDefault()
      const res = await fetch(`http://localhost:3000/register?name=${name}&email=${email}&password=${password}`,
        {
          method: 'POST'
        }
      )
      const data = await res.text()
      toast(data)
    }
    
    const handleLogin = async (e) => {
      e.preventDefault()
      const res = await fetch(`http://localhost:3000/login?email=${email}&password=${password}`,
        {
          method: 'POST'
        }
      )
      const data = await res.json()
      if (data.ok) {
        setCookies('passwordHashed', data.password)
        toast(data.message)
      } else {
        toast(data.message)
      }
    }
    return (
        <>
        <div>
            <form onSubmit={handleRegister}>
                <input placeholder='name' type="text" name='name' onChange={(e) => setName(e.target.value)} />
                <input placeholder='email' type="email" name='email' onChange={(e) => setEmail(e.target.value)} />
                <input placeholder='password' type="password" name='password' onChange={(e) => setPassword(e.target.value)} />
                <button type="submit">Register</button>
            </form>
            <form onSubmit={handleLogin}>
                <input placeholder='email' type="email" name='email' onChange={(e) => setEmail(e.target.value)} />
                <input placeholder='password' type="password" name='password' onChange={(e) => setPassword(e.target.value)} />
                <button type="submit">Login</button>
            </form>
        </div>
        <ToastContainer />
        </>
    );
}

export default Forms;