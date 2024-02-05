import { useEffect } from "react"

function Users({value, filteredUserArray, setFilteredUserArray, setState, setUserId, setValue}){
    useEffect(() =>{
        const getData = async () => {
          try {
            const res = await fetch(`https://jsonplaceholder.typicode.com/users`)
            const data = await res.json()
            console.log(data)
            setFilteredUserArray(data.filter((item) => item.name.startsWith(value)))
          } catch (err) {
            console.log(err)
          }
        }
        getData()
    }, [value])
    return(
        <>
        <input type="text" onChange={(val) => setValue(val.target.value)}/>
        <div className='box'>
            {filteredUserArray.map((item) => {
            return(
            <div key={item.id}>
                <p>{item.name}</p>
                <p>{item.username}</p>
                <p>{item.email}</p>
                <button onClick={() => {
                    setState(true)
                    setUserId(item.id)
                }}>View Posts</button>
            </div>
            )
            })}
        </div>
        </>
    )
}

export default Users