import { useEffect } from "react"

function UserData({user, setState}){
    console.log(user);
    return(
        <>
        <button onClick={() => setState(false)}>Back</button>
        <div className='box'>
            <div>
                <img src={user.photoUrl} alt={user.name}/>
                <p>{user.name}</p>
                <p>{user.username}</p>
                <p>{user.email}</p>
                <p>{user.phone}</p>
                <p>{user.website}</p>
                <h1>Address:</h1>
                <p>{user.address.street}</p>
                <p>{user.address.suite}</p>
                <p>{user.address.city}</p>
                <p>{user.address.zipcode}</p>
                <p>{user.address.geo.lat}</p>
                <p>{user.address.geo.lng}</p>
                <h1>Company:</h1>
                <p>{user.company.name}</p>
                <p>{user.company.catchPhrase}</p>
                <p>{user.company.bs}</p>
                
            </div>
        </div>
        </>
    )
}

export default UserData