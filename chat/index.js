const cors = require('cors')
const express = require('express')
const fs = require('fs')
const fileUpload = require("express-fileupload");
const mssql = require('mssql')

const connectionString = {
    user: 'db_aaa70b_magab19_admin',
    password: 'Fl1ck_Maga',
    server: 'SQL8010.site4now.net',
    database: 'db_aaa70b_magab19',
    options: {
        encrypt: false,
        enableArithAbort: true
    }
}

const bodyParser = require('body-parser')
let app = express()
let HOST = 3000
app.use(cors())
app.use(bodyParser.json())
app.use(bodyParser.urlencoded({ extended: true }));

app.use('/static', express.static('public')); 

app.post('/create-or-choose-chat', (req, res) => {
    if(req.body){
        let chat = req.body.chat
        mssql.connect(connectionString, (err) => {
            if(err){
                console.log(err)
            }else{
                let request = new mssql.Request()
                request.query(`SELECT * FROM Chats WHERE chat_name = '${chat}'`, (err, result) => {
                    if(err){
                        console.log(err)
                    }else{
                        if(result.recordset.length === 0){
                            request.query(`INSERT INTO Chats VALUES (${result.recordset.length + 1}, '${chat}')`, (err) => {
                                if(err){
                                    console.log(err)
                                }else{
                                    res.json({text:`Chat ${chat} was created`})
                                }
                            })
                        }else{
                            request.query(`SELECT * FROM Messages WHERE chat_id = ${result.recordset[0].chat_id}`, (err, result) => {
                                if(err){
                                    console.log(err)
                                }else{
                                    res.json({array:result.recordset})
                                }
                            })
                        }
                    }
                })
            }
        })
    }
});

app.post('/letter-sending', (req,res) => {
    if(req.body){
        let letter = req.body
        mssql.connect(connectionString, (err) => {
            if(err){
                console.log(err)
            }else{
                let request = new mssql.Request()
                request.query(`SELECT * FROM Chats WHERE chat_name = '${letter.chat}'`, (err, result) => {
                    if(err){
                        console.log(err)
                    }else{
                        let chat_id = result.recordset[0].chat_id
                        request.query(`SELECT * FROM Messages WHERE chat_id = ${chat_id}`, (err, result) => {
                            if(err){
                                console.log(err)
                            }else{
                                letter.message_id = result.recordset.length + 1
                                let date = new Date()
                                letter.time = `${date.getFullYear()}-${date.getMonth() + 1}-${date.getDate()} ${date.getHours()}:${date.getMinutes()}:${date.getSeconds()}`
                                let query = `INSERT INTO Messages VALUES (${letter.message_id}, ${chat_id}, '${letter.letter}', null, '${letter.from}', '${letter.to}', '${letter.time}')`
                                request.query(query, (err) => {
                                    if(err){
                                        console.log(err)
                                    }else{
                                        request.query(`SELECT * FROM Messages WHERE chat_id = ${chat_id}`, (err, result) => {
                                            if(err){
                                                console.log(err)
                                            }else{
                                                res.json({array:result.recordset})
                                            }
                                        });
                                    }
                                })
                            }
                        })
                    }
                })
            }
        })
    }
});

app.post('/send-photo', fileUpload({ createParentPath: true }), (req, res) => {
    if(req.body){
        // get message and chat name from query
        let message = req.query.message
        let from = req.query.from
        let to = req.query.to
        let time = req.query.time
        let chat_name = from + 'and' + to
        let file = req.files
        Object.keys(file).forEach(key => {
            file = file[key]
        })
        let data = file.data
        mssql.connect(connectionString, (err) => {
            if(err){
                console.log(err)
            }else{
                let request = new mssql.Request()
                request.query(`SELECT * FROM Chats WHERE chat_name = '${chat_name}'`, (err, result) => {
                    if(err){
                        console.log(err)
                    }else{
                        let chat_id = result.recordset[0].chat_id
                        request.query(`SELECT * FROM Messages WHERE chat_id = ${chat_id}`, (err, result) => {
                            if(err){
                                console.log(err)
                            }else{                             
                                let message_id = result.recordset.length + 1
                                let query = `INSERT INTO Messages VALUES (${message_id}, ${chat_id}, '${message}', ${data}, '${from}', '${to}', '${time}')`
                                request.query(query, (err) => {
                                    if(err){
                                        console.log(err)
                                    }else{
                                        request.query(`SELECT * FROM Messages WHERE chat_id = ${chat_id}`, (err, result) => {
                                            if(err){
                                                console.log(err)
                                            }else{
                                                res.json({array: result.recordset})
                                                console.log(result.recordset)
                                            }
                                        })
                                    }
                                })
                            }
                        })
                    }
                })
            }
        }
        )
    }
});

app.get('/getImage', (req, res) => {
    let bufferBase64 = req.query.bufferBase64
    let buffer = Buffer.from(bufferBase64, 'base64')
    let date = new Date()
    fs.writeFile(`./public/images/${date.getFullYear()}-${date.getMonth() + 1}-${date.getDate()} ${date.getHours()}:${date.getMinutes()}:${date.getSeconds()}.png`, buffer, (err) => {
        if(err){
            console.log(err)
        }else{
            fs.readFile(`./public/images/${date.getFullYear()}-${date.getMonth() + 1}-${date.getDate()} ${date.getHours()}:${date.getMinutes()}:${date.getSeconds()}.png`, (err, data) => {
                if(err){
                    console.log(err)
                }else{
                    res.send(data)
                    fs.unlink(`./public/images/${date.getFullYear()}-${date.getMonth() + 1}-${date.getDate()} ${date.getHours()}:${date.getMinutes()}:${date.getSeconds()}.png`, (err) => {
                        if(err){
                            console.log(err)
                        }
                    })
                }
            })
        }
    })
})

app.delete('/clearAllData', (req, res) => {
    mssql.connect(connectionString, (err) => {
        if(err){
            console.log(err)
        }else{
            let request = new mssql.Request()
            request.query('DELETE FROM Messages', (err) => {
                if(err){
                    console.log(err)
                }else{
                    request.query('DELETE FROM Chats', (err) => {
                        if(err){
                            console.log(err)
                        }else{
                            res.json({text:'All data was cleared'})
                        }
                    })
                }
            })
        }
    })
})

// app.post('/save-photo', (req, res) => {
// })

// app.post('/create-or-choose-chat', (req, res) => {
//     if(req.body){
//         console.log(req.body)
//         files = fs.readdirSync('./chatFiles')
//         console.log(files)
//         if (files.includes(req.body.chat + '.json')) {
//             files.forEach((item) => {
//                 if (req.body.chat + '.json' === item) {
//                     console.log(item)
//                     fs.readFile(`./chatFiles/${item}`, 'utf-8', (err, data) => {
//                         if (err) {
//                             console.log(err)
//                         } else {
//                             let array = JSON.parse(data)
//                             res.json({array:array})
//                         }
//                     })
    
//                 }
//             })
    
//         } else {
//             let chatArray = JSON.stringify([])
//             fs.writeFile(`./chatFiles/${req.body.chat}.json`, chatArray, 'utf-8', (err) => {
//                 if (err) {
//                     console.log(err)
//                 } else {
//                     res.json({text:`Chat ${req.body.chat} was created`})
//                 }
//             })
//         }
//     }
    
// })

// app.get('/getImage/:name', (req, res) => {
//     res.send('asd')
// });

// app.post('/letter-sending', (req,res) => {
//     if(req.body){
//         console.log(req.body)
//         let letter = req.body
//         fs.readFile(`./chatFiles/${letter.chat}.json`, 'utf-8', (err, data) => {
//             if (err) {
//                 console.log(err)
//             } else {
//                 let array = JSON.parse(data)
//                 console.log(array)
//                 if(array.length === 0){
//                     letter.id = 1
//                 }else{
//                     letter.id = array.length + 1
//                 }
    
//                 array.push(req.body)
//                 stringifyedArray = JSON.stringify(array)
//                 fs.writeFile(`./chatFiles/${letter.chat}.json`, stringifyedArray, 'utf-8', (err) => {
//                     if (err) {
//                         console.log(err)
//                     } else {
//                         res.json(array)
//                     }
//                 })
//             }
//         })
//     }
// })

app.listen(HOST, () => {
    console.log(`http://localhost:${HOST}`)
})