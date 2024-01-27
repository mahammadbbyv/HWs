import modalImg1 from "../../assets/modalImg1.png"
import modalImg2 from "../../assets/modalImg2.png"
import modalImg3 from "../../assets/modalImg3.png"
import "./styles/Admin.css"

function ModalWindow({setShowModal, showModal}){        
    if(showModal == 1)
    {return (
        <div className="modal-container">
        <div className="modal">
            <button className="modalExit" onClick={() => setShowModal(false)}>X</button>
            <img src={modalImg1} alt="maps1" className="img1" />
            <h2>1. Search your own location</h2>
            <h2>2. Press "Share"</h2>
            <img src={modalImg2} alt="maps2" className="img2" />
            <h2>3. Press "Embed a map"</h2>
            <h2>4. Press "COPY HTML"</h2>
            <h2>5. Paste the code in the &lt;iframe... field.</h2>
        </div>
        </div>
    )}else if(showModal == 2){
        return (
            <div className="modal-container">
            <div className="modal">
                <button className="modalExit" onClick={() => setShowModal(0)}>X</button>
                <h2>Send either one of us an email with your pharmacy's id<br /> in order to get verified.<br />you can see an id of your pharmacy here:</h2>
                <img src={modalImg3} alt="maps3" className="img3" />
            </div>
            </div>
        )
    }
}

export default ModalWindow