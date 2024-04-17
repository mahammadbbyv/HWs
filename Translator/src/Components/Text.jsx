import { useEffect, useState } from "react";

function Text({continuous, finished}){
    let [lastContinuous, setLastContinuous] = useState("");
    let [lastFinished, setLastFinished] = useState("");
    const [text, setText] = useState("");
    useEffect(() => {
        if(finished === ""){
            setText(continuous);
            setLastContinuous(continuous);
        }
        else if(continuous !== lastContinuous && finished === lastFinished){
            setText(continuous);
            setLastContinuous(continuous);
        }else if(finished !== lastFinished){
            setText(finished);
            setLastFinished(finished);
        }
    }, [continuous, finished]);
    return (
        <div style={{width: "500px", height: "400px", background: "gray", color: "white"}} className="recognized">
            <p style={{margin: "20px"}}>
                {text}
            </p>
        </div>
    )
}

export default Text;