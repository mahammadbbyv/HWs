const searchBtn = document.querySelector('#search-btn');
const heading = document.querySelector('#heading');
console.log(heading.innerText);

searchBtn.addEventListener('click', async () => {

    let input = document.querySelector('#city-name');
    let city = input.value;
    let url = `https://api.openweathermap.org/data/2.5/weather?q=${city}&appid=2b1fd2d7f77ccf1b7de9b441571b39b8&units=metric`;

    let res = await fetch(url);
    let data = await res.json();
    
    for(let key in data){
        if(typeof data[key] === 'object' && typeof data[key]["0"] !== 'object'){
            for(let key2 in data[key]){
                if(key2 != "id"){
                    document.getElementById(`${key2}`).innerText = `${key} ${key2}: ${data[key][key2]}`;
                }
            }
        }else if(typeof data[key] === 'object' && typeof data[key]["0"] === 'object'){
            for(let key2 in data[key]["0"]){
                if(key2 != "id"){
                    document.getElementById(`${key2}`).innerText = `${key} ${key2}: ${data[key][key2]}`;
                }
            }
        }else{
            document.getElementById(`${key}`).innerText = `${key}: ${data[key]}`;
        }
    }
    input.value = '';
});





