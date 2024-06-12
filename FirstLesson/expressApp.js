const express = require('express');
const cors = require('cors');
const app = express();
app.use(cors());
let array = ["asd", "qwe", "zxc"];

app.get('/', (req, res) => {
    res.send(array.map((v, i) => ({id: i, value: v})));
});

app.put('/add/:data', (req, res) => {
    array.push(req.params.data);
    res.send(array.map((v, i) => ({id: i, value: v})));
});

app.delete('/delete/:index', (req, res) => {
    array.splice(req.params.index, 1);
    res.send(array.map((v, i) => ({id: i, value: v})));
});

app.put('/edit/:index/:data', (req, res) => {
    array[req.params.index] = req.params.data;
    res.send(array.map((v, i) => ({id: i, value: v})));
});

app.listen(3000, () => {
    console.log('Server is running on port 3000');
});