const express = require('express');
const app = express();
const cors = require('cors');
app.use(cors());
const bcrypt = require('bcrypt');
let array = [];

app.get('/verify', (req, res) => {
    let item = array.filter((v) => v.password == req.query.password);
    if (item.length == 0) {
        res.send({ok: false, message: "Password not found"});
    } else {
        res.send({ok: true, message: "Password found", data: item[0]});
    }
});

app.post('/register', (req, res) => {
    let item = {id: array.length, name: req.query.name, email: req.query.email, password: bcrypt.hashSync(req.query.password, 10)};
    console.log(item);
    array.push(item);
    res.send("User registered.");
});

app.post('/login', (req, res) => {
    let item = array.filter((v) => v.email == req.query.email);
    if (item.length == 0) {
        res.send("User not found");
    } else {
        if (bcrypt.compareSync(req.query.password, item[0].password)) {
            res.send({ok: true, message: "Logged in successfully", data: array, password: item[0].password});
        } else {
            res.send({ok: false, message: "Incorrect password", data: null, password: null});
        }
    }
});

app.post('/delete', (req, res) => {
    let item = array.filter((v) => v.email == req.query.email);
    if (item.length == 0) {
        res.send("User not found");
    } else {
        if(!bcrypt.compareSync(req.query.password, item[0].password)) {
            res.send("Incorrect password.");
        }
        array = array.filter((v) => v.email != req.query.email);
        res.send("User deleted");
    }
});

app.listen(3000, () => {
    console.log("Server is running on port 3000");
});