import { useEffect, useState } from "react";
import { getDistance } from "geolib";

function Main(){
    const [latitude, setLatitude] = useState(0.0);
    const [longitude, setLongitude] = useState(0.0);

    // useEffect(() => {
    //         if (navigator.geolocation) {
    //         navigator.geolocation.getCurrentPosition(
    //             (position) => {
    //                 setLatitude(position.coords.latitude);
    //                 setLongitude(position.coords.longitude);
    //                 localStorage.setItem('lat', position.coords.latitude);
    //                 localStorage.setItem('lon', position.coords.longitude);
                    
    //                 
    //             },
    //             (error) => {
    //             }
    //         );
    //         } else {
    //         console.error('Geolocation is not supported by this browser.');
    //         }
        
    // }, []);
    const [address, setAddress] = useState("");
    const [value, setValue] = useState("");
    useEffect(() =>{
        const getData = async () => {
          try {
            const res = await fetch(`https://api.geoapify.com/v1/geocode/search?text=${value}&apiKey=7c2a1a92d80e49f180b2a074ec5527d0`)
            const data = await res.json()
            console.log(data.features[0].properties.lat + ", " + data.features[0].properties.lon)
            if (navigator.geolocation) {
              navigator.geolocation.getCurrentPosition(
                  (position) => {
                  setLatitude(position.coords.latitude);
                  setLongitude(position.coords.longitude);
                  console.log(getDistance(
                      { latitude: latitude, longitude: longitude },
                      { latitude: data.features[0].properties.lat, longitude: data.features[0].properties.lon }
                  ))
                },
                (error) => {
                }
              );
            } else {
              console.error('Geolocation is not supported by this browser.');
            }
          } catch (err) {
            console.log(err)
          }
        }
        getData()
    }, [value])
    return (
        <main>
            <div>
                <input type="text" placeholder="Enter your address..." onChange={(ev) => setAddress(ev.target.value)} />
                <button onClick={() => setValue(address)}>Search</button>
                <p>{latitude}, {longitude}</p>
            </div>
        </main>
    )
}

export default Main