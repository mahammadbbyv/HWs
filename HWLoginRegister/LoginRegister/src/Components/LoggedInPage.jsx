/* eslint-disable react-hooks/exhaustive-deps */
/* eslint-disable react/prop-types */
/* eslint-disable no-unused-vars */
import { useState } from "react";
import { useEffect } from "react";
import { useCookies } from "react-cookie";

function LoggedInPage() {
    const [name, setName] = useState('')
    const [email, setEmail] = useState('')
    const [cookies, setCookies, removeCookies] = useCookies(['passwordHashed']);

    const handleDelete = async (e) => {
        e.preventDefault()
        const res = await fetch(`http://localhost:3000/delete?email=${email}&password=${cookies.passwordHashed}`,
            {
                method: 'POST'
            }
        )
        const data = await res.text()
        alert(data)
        if (data === 'User deleted') {
            removeCookies('passwordHashed')
            window.location.href = '/'
        }
    }

    useEffect(() => {
        if (!cookies.passwordHashed) {
            window.location.href = '/'
        }
        fetch(`http://localhost:3000/verify?password=${cookies.passwordHashed}`)
            .then(response => response.json())
            .then(data => {
                if (!data.ok) {
                    removeCookies('passwordHashed')
                    window.location.href = '/'
                }else{
                    setName(data.data.name)
                }
            })
            .catch(error => console.error(error));
    }, [cookies.passwordHashed])
    return (
        <div>
            <h1>Welcome to the LoggedInPage! {name}</h1>
            <button onClick={() => removeCookies('passwordHashed')}>Logout</button>
            <input type="email" placeholder="Email" onChange={(e) => setEmail(e.target.value)} />
            <button onClick={handleDelete}>Delete Account</button>
        </div>
    );
}

export default LoggedInPage;