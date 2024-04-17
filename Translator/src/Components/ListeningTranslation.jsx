import React, { useState, useEffect, useRef } from 'react';
import * as sdk from 'microsoft-cognitiveservices-speech-sdk';
import './Styles/ListeningTranslation.css';
import Text from './Text';
import LanguageSelector from './LanguageSelector';
import { FaMicrophone } from "react-icons/fa";
const SPEECH_KEY = '';
const SPEECH_REGION = '';

function ListeningTranslation({setText, setLang}) {

  const [isListening, setIsListening] = useState(false);
  const speechConfig = useRef(null);
  const audioConfig = useRef(null);
  const recognizer = useRef(null);

  const [myTranscript, setMyTranscript] = useState("");
  const [recognizingTranscript, setRecTranscript] = useState("");
  const [language, setLanguage] = useState("en-US");

  useEffect(() => {
    setLang(language);
    speechConfig.current = sdk.SpeechConfig.fromSubscription(
      SPEECH_KEY,
      SPEECH_REGION
    );
    speechConfig.current.speechRecognitionLanguage = language;

    audioConfig.current = sdk.AudioConfig.fromDefaultMicrophoneInput();
    recognizer.current = new sdk.SpeechRecognizer(
      speechConfig.current,
      audioConfig.current
    );

    const processRecognizedTranscript = (event) => {
      const result = event.result;

      if (result.reason === sdk.ResultReason.RecognizedSpeech) {
        const transcript = result.text;

        setMyTranscript(transcript);
        setText(transcript);
      }
    };

    const processRecognizingTranscript = (event) =>{
        const result = event.result;
        if (result.reason === sdk.ResultReason.RecognizingSpeech) {
            const transcript = result.text;
    
            setRecTranscript(transcript);
            setText(transcript);
        }
    }

    recognizer.current.recognized = (s, e) => processRecognizedTranscript(e);
    recognizer.current.recognizing = (s, e) => processRecognizingTranscript(e);

    return () => {
      recognizer.current.stopContinuousRecognitionAsync(() => {
        setIsListening(false);
      });
    };
  }, [language]);

  const startStop = () => {
    setIsListening(!isListening);
    if (isListening) {
      setIsListening(false);
      recognizer.current.stopContinuousRecognitionAsync(() => {
        console.log('Speech recognition stopped. ' + language);
      });
    }else{
      setIsListening(true);
      recognizer.current.startContinuousRecognitionAsync(() => {
        console.log('Resumed listening... ' + language);
      });
      setText("");
    }
  };

  return (
    <div style={{display: "flex", alignItems: "center", flexDirection: "column", height: "500px", justifyContent: "space-between"}}>
      <div style={{display: "flex", flexDirection: "row", alignItems:"center", justifyContent:"space-between", width: "100%"}}>
        <div className={'listening ' + (isListening ? 'active' : 'inactive')}>
          <FaMicrophone size={40} onClick={startStop} />
        </div>
        <div style={{color: "black", width: "150px"}}>
          <LanguageSelector lang={setLanguage} />
        </div>
      </div>
      <Text continuous={recognizingTranscript} finished={myTranscript} />
    </div>
  );
};

export default ListeningTranslation;
