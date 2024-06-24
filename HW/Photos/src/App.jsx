import { useState, useEffect } from 'react'
import './App.css'

function App() {
  const [photo, setPhoto] = useState({
    url: '',
    description: ''
  })
  const [photos, setPhotos] = useState([])
  useEffect(() => {
    fetch('http://localhost:3000/getPhotos')
      .then(res => res.json())
      .then(data => setPhotos(data))
  })
  const handleSubmit = async (e) => {
    e.preventDefault()
    const res = await fetch(`http://localhost:3000/addPhoto?url=${photo.url}&description=${photo.description}`,
      {
        method: 'POST'
      }
    )
    const data = await res.json()
    setPhotos([data])
  }
  return (
    <>
    <div>
      <form onSubmit={handleSubmit}>
        <input placeholder='url' type="text" name='url' onChange={(e) => setPhoto({...photo, url: e.target.value})} />
        <input placeholder='description' type="text" name='description' onChange={(e) => setPhoto({...photo, description: e.target.value})} />
        <button type="submit">Submit</button>
      </form>
      <div>
        {photos.map((photo) => (
          <div key={photo.id}>
            <img src={photo.url} alt={photo.description} />
            <p>{photo.description}</p>
          </div>
        ))}
      </div>
    </div>
    </>
  )
}

export default App
