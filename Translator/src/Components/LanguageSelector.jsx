import Select from 'react-select'

function LanguageSelector({lang}){
    const options = [
        { value: 'az-AZ', label: 'Azerbaijani' },
        { value: 'en-US', label: 'English' },
        { value: 'es-ES', label: 'Spanish' },
        { value: 'fr-FR', label: 'French' },
        { value: 'de-DE', label: 'German' },
        { value: 'it-IT', label: 'Italian' },
        { value: 'ja-JP', label: 'Japanese' },
        { value: 'ko-KR', label: 'Korean' },
        { value: 'pt-BR', label: 'Portuguese' },
        { value: 'ru-RU', label: 'Russian' },
        { value: 'zh-CN', label: 'Chinese' }
    ]
    function onChangeHandler(e){
        lang(e.value);
    }
    return (
        <Select options={options} onChange={onChangeHandler} defaultValue={options[1]} />
    )
}

export default LanguageSelector;