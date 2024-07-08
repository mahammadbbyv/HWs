let friendsList = ['Taleh', 'Tamerlan', 'Alexandr']
let me = 'Ramin'

let friends = document.getElementById('friends')

friendsList.forEach((item) => {
    let li = document.createElement('li')
    li.innerHTML = `<button>${item}</button>`
    li.addEventListener('click', () => {
        chooseChat(item)
        fetch('http://localhost:3000/create-or-choose-chat', {
            method: "POST",
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ chat: `${me}and${item}` })
        })
            .then(res => res.json())
            .then(data => {
                let chatList = document.getElementById('chatList')
                chatList.innerHTML = ''
                if (data.array) {
                    data.array.forEach((item) => {
                        let div = document.createElement('div')
                        div.classList.add(item.from === me ? 'from' : 'to')
                        if(item.file !== null){
                            let img = document.createElement('img');
                            img.src = `http://localhost:3000/getImage/${item.file}`;
                            div.appendChild(img);
                        }
                        let p = document.createElement('p')
                        p.innerText = item.letter + '---' + item.time
                        div.appendChild(p)
                        chatList.appendChild(div)
                    })


                } else if (data.text) {
                }
            })
        let fromMeForm = document.getElementById('fromMeForm')
        let fromFriendForm = document.getElementById('fromFriendForm')


        fromMeForm.addEventListener('submit', (ev) => {
            ev.preventDefault()
            let fromMeFormInput = document.querySelector('#fromMeForm input').value
            let date = new Date()
            let year = date.getFullYear()
            let month = date.getMonth() + 1
            let day = date.getDate()
            let hours = date.getHours()
            let minutes = date.getMinutes()
            let seconds = date.getSeconds()
            let fileInput = document.querySelector('#fromMeForm input[type="file"]');
            let file = fileInput.files;
            let type = null;
            let fromMeLetter = {
                letter: fromMeFormInput,
                file: !file ? null : `${seconds}-${minutes}-${hours}-${day}-${month}-${year}.${type}`,
                from: me,
                to: item,
                time: `${year}/${month}/${day} - ${hours}:${minutes}:${seconds}`,
                chat: me + "and" + item
            }
            try{
                type = file.type.split('/')[1];
            }
            catch{
                type = null;
            }
            if (file.length > 0) {
                const formData = new FormData();
                Object.keys(file).forEach(key => {
                    formData.append(file.item(key).name, file.item(key))
                })
                let chat = me + "and" + item;
                let query = {
                    message: fromMeFormInput,
                    from: me,
                    to: item,
                    time: `${year}/${month}/${day} - ${hours}:${minutes}:${seconds}`,
                    chat_name: chat
                }
                let queryString = Object.keys(query).map(key => key + '=' + query[key]).join('&').replace(/ /g, '%20');
                fetch(`http://localhost:3000/send-photo?${queryString}`, {
                    method: 'POST',
                    body: formData
                }).then(res => res.json()).then(data => {
                    let chatList = document.getElementById('chatList')
                    if (data) {
                        chatList.innerHTML = ''
                        data.array.forEach((item) => {
                            let div = document.createElement('div')
                            div.classList.add(item.from === me ? 'from' : 'to')
                            if(item.file !== null){
                                console.log(item.file)
                            }
                            let p = document.createElement('p')
                            p.innerText = item.letter + '---' + item.time
                            div.appendChild(p)
                            chatList.appendChild(div)
                        })
                    }
                })
            }
            else{
                fetch('http://localhost:3000/letter-sending', {
                    method: 'POST',
                    headers: {
                        'Content-type': 'application/json'
                    },
                    body: JSON.stringify(fromMeLetter)
                })
                .then(res => res.json())
                .then(data => {
                    let chatList = document.getElementById('chatList')
                    if (data) {
                        chatList.innerHTML = ''
                        data.array.forEach((item) => {
                            let div = document.createElement('div')
                            div.classList.add(item.from === me ? 'from' : 'to')
                            if(item.file !== null){
                                let img = document.createElement('img');
                                img.src = `http://localhost:3000/getImage/${item.file}`;
                                div.appendChild(img);
                            }
                            let p = document.createElement('p')
                            p.innerText = item.letter + '---' + item.time
                            div.appendChild(p)
                            chatList.appendChild(div)
                        })
                    }
                })
            }
        })

        fromFriendForm.addEventListener('submit', (ev) => {
            ev.preventDefault()
            let fromFriendInput = document.querySelector('#fromFriendForm input').value
            let date = new Date()
            let year = date.getFullYear()
            let month = date.getMonth() + 1
            let day = date.getDate()
            let hours = date.getHours()
            let minutes = date.getMinutes()
            let seconds = date.getSeconds()
            const fileInput = document.querySelector('#fromFriendForm input[type="file"]');
            let tmp = fileInput.files[0];
            let type = null;
            try{
                type = tmp.type.split('/')[1];
            }
            catch{
                type = null;
            }
            let fromFriendLetter = {
                letter: fromFriendInput,
                file: !tmp ? null : `${seconds}-${minutes}-${hours}-${day}-${month}-${year}.${type}`,
                from: item,
                to: me,
                time: `${year}/${month}/${day} - ${hours}:${minutes}:${seconds}`,
                chat: me + "and" + item
            }
            if(tmp){
                let file = new File([tmp], `${seconds}-${minutes}-${hours}-${day}-${month}-${year}.${type}`, { type: tmp.type });
                const formData = new FormData();
                let chat = me + "and" + item;
                formData.append('message', fromFriendInput);
                formData.append("chat_name", chat);
                formData.append("files", file);
                fetch(`http://localhost:3000/send-photo`, {
                    method: 'POST',
                    body: formData
                })
            }else{
                fetch('http://localhost:3000/letter-sending', {
                    method: 'POST',
                    headers: {
                        'Content-type': 'application/json'
                    },
                    body: JSON.stringify(fromFriendLetter)
                })
                .then(res => res.json())
                .then(data => {
                    let chatList = document.getElementById('chatList')
                    
                    if (data) {
                        chatList.innerHTML = ''
                        data.array.forEach((item) => {
                            let div = document.createElement('div')
                            div.classList.add(item.from === me ? 'from' : 'to')
                            if(item.file !== null){
                                let img = document.createElement('img');
                                img.src = `http://localhost:3000/getImage/${item.file}`;
                                div.appendChild(img);
                            }
                            let p = document.createElement('p')
                            p.innerText = item.letter + '---' + item.time
                            div.appendChild(p)
                            chatList.appendChild(div)
                        })
                    }
                    
                    
                })
            }
            })
        })
        friends.appendChild(li)
})

function chooseChat(friend) {
    let chatContainer = document.getElementById('chatContainer')
    chatContainer.innerHTML = `
<div id="chat">
    <ul id="chatList"></ul>
</div>
<form id="fromMeForm">
    <input type="text" id="fromMeInput">
    <input type="file" name='files' id="photoInput" accept="image/*">

    <button>SEND TO ${friend}</button>
</form>
<form id="fromFriendForm">
    <input type="text" id="toFriendInput">
    <input type="file" name='files' id="photoInput" accept="image/*">
    <button>SEND TO ${me}</button>
</form>`
}