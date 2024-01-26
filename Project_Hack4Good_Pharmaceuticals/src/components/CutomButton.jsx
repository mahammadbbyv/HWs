import { Link } from "react-router-dom"
import "./styles/CustomButton.css"

function CustomButton({text, linkText, func = null, email = null, phoneNumber = null, password = null, confirmPassword = null}){
    return(
        <>
            <Link className="btn" to={`/${linkText}`} onClick={() =>{
                if(func != null){
                    if(confirmPassword != null){
                        func(email, phoneNumber, password, confirmPassword);
                    }else{
                        func(email, password);
                    }
                }
            }}>{text}</Link>
        </>
    )
}

export default CustomButton