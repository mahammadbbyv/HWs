// const express = require('express');
// const bodyParser = require('body-parser');

// const app = express();
// const PORT = 3000;

// app.use(bodyParser.json());

// let users = ["asd"];

// app.get('/users', (req, res) => {
//   res.json(users);
// });

// app.post('/users', (req, res) => {
//   const user = req.body;
//   users.push(user);
//   res.status(201).json(user);
// });

// app.listen(PORT, () => {
//   console.log(`Server is running on port ${PORT}`);
// });

exports.handler = async (event) => {
  // Your API logic here
  const response = {
      statusCode: 200,
      body: JSON.stringify('Hello from your API!'),
  };
  return response;
};