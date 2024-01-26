import "./styles/Card.css"

function Card({title, text, imgUrl}){

    return(
        <>
        <div className="card">
            <div className="card-main">
                <div className="card-text">
                    <h4 className="card-title">{title}</h4>
                    <span className="card-content">{text}</span>
                </div>
            </div>

            <img className="card-img" src={imgUrl}/>
        </div>
        </>
    )
}

export default Card