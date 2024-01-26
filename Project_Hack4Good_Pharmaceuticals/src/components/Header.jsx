import { Link } from "react-router-dom"
import './styles/Header.css'
import caduceus from '../assets/caduceus.png'
import CustomButton from "./CutomButton"

function Header(){
    return(
        <div className="header">
            <Link to={"/"} className="logo">
                <span className="logo-text"><h1>P</h1><img src={caduceus} className="i-logo-image"/><h1>YA</h1></span>
            </Link>
            <ul className="navbar">
                <li className="search-btn"><Link to={"/search"}>🔍  Search</Link></li>
                <li><CustomButton text={"Join"} linkText={"login"}/></li>
            </ul>
        </div>
    )
}

export default Header