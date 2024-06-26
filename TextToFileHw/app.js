const express = require('express');
const app = express();
const cors = require('cors');
app.use(cors());
const fs = require('fs');

const path = './TextFiles/';

app.post('/textsToFile', (req, res) => {
    let totalArray = [];
    let buffer = Buffer.from(req.query.text1);
    let json = buffer.toJSON();
    totalArray = totalArray.concat([...json.data]);
    buffer = Buffer.from(req.query.text2);
    json = buffer.toJSON();
    totalArray = totalArray.concat([...json.data]);
    buffer = Buffer.from(req.query.text3);
    json = buffer.toJSON();
    totalArray = totalArray.concat([...json.data]);
    buffer = Buffer.from(req.query.text4);
    json = buffer.toJSON();
    totalArray = totalArray.concat([...json.data]);
    buffer = Buffer.from(req.query.text5);
    json = buffer.toJSON();
    totalArray = totalArray.concat([...json.data]);
    let result = '';
    for (let i = 0; i < 1000 && i < totalArray.length; i++) {
        result += String.fromCharCode(totalArray[i]);
    }
    let time = new Date().getTime();
    fs.writeFile(path + `${time}.txt`, result, (err) => {
        if (err) {
            res.send('Error');
        }
    });
    fs.readFile(path + `${time}.txt`, 'utf8', (err, data) => {
        if (err) {
            res.send('Error');
        }
        res.send(data);
    });
});

app.listen(3000, () => {
    console.log("Server is running on port 3000");
});