const express = require('express');
const cors = require('cors');
const app = express();
app.use(cors());
const usersRouter = require('./users');

app.use(express.static('public'));
app.use('/users', usersRouter);

app.listen(3000, () => {
    console.log('Server is running on http://localhost:3000');
});
