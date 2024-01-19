import { useEffect } from "react"

function Posts({value, filteredPostArray, setFilteredPostArray, setState, userId}){
    useEffect(() =>{
        const getData = async () => {
          try {
            const res = await fetch(`https://jsonplaceholder.typicode.com/posts`)
            const data = await res.json()
            console.log(data)
            setFilteredPostArray(data.filter((item) => item.userId == userId))
          } catch (err) {
            console.log(err)
          }
        }
        getData()
    }, [value])
    return(
        <>
        <button onClick={() => setState(false)}>Back</button>
        <div className='box'>
            {filteredPostArray.map((item) => {
            return(
            <div key={item.id}>
                <p>{item.title}</p>
                <p>{item.body}</p>
            </div>
            )
            })}
        </div>
        </>
    )
}

export default Posts