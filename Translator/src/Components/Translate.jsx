import { useEffect, useState } from "react"
import { v4 as uuidv4 } from 'uuid';
import Text from "./Text";
import LanguageSelector from "./LanguageSelector";

function Translate({sentence, language}){
    const [translatedText, setTranslatedText] = useState("");
    const [translate, setTranslate] = useState("en-US");
    const subscriptionKey = "";
    const endpoint = "https://api.cognitive.microsofttranslator.com";
    const location = "";
    const path = "/translate";
    const url = endpoint + path;
    useEffect(() => {
        const myArray = translate.split("-");
        setTranslate(myArray[0] + (myArray[0] == "zh" ? "-CN" : ""))
        if(sentence !== ""){
            const query = new URLSearchParams({
                'api-version': '3.0',
                "to": [myArray[0]],
            });
            fetch(url + "?" + query , {
                method: "POST",
                headers: {
                    "Ocp-Apim-Subscription-Key": subscriptionKey,
                    "Ocp-Apim-Subscription-Region": location,
                    "Content-Type": "application/json",
                    "X-ClientTraceId": uuidv4().toString(),
                },
                body: JSON.stringify([{text: sentence}])
            })
            .then(response => response.json())
            .then(data => {
                console.log(data);
                setTranslatedText(data[0].translations[0].text);
            })
            .catch(err => console.log(err));
        }
    }, [sentence, language, translate]);
    return (
        <div style={{display: "flex", alignItems: "center", flexDirection: "column", height: "500px", justifyContent: "space-between"}}>
            <div style={{display: "flex", flexDirection: "column", height: "max-height", justifyContent: "center"}}>
                <div style={{color: "black", width: "150px"}}>
                    <LanguageSelector lang={setTranslate} />
                </div>
            </div>
            <Text continuous={translatedText} />
        </div>
    )
}

export default Translate;
