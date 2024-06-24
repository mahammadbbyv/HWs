const express = require('express');
const app = express();
const cors = require('cors');
app.use(cors());
let array = [];

app.post('/addPhoto', (req, res) => {
    let item = {id: array.length, url: req.query.url, description: req.query.description};
    console.log(item);
    array.push(item);
    res.send(array);
});

app.get('/getPhotos', (req, res) => {
    res.send(array);
});

app.listen(3000, () => {
    console.log("Server is running on port 3000");
});