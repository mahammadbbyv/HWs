function Second({ obj }){
    console.log(obj);
    return (
        <div>
            <p>{obj.title}</p>
            <p>{obj.description}</p>
        </div>
    )
}

export default Second