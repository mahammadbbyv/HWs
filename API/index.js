const express = require('express');
const bodyParser = require('body-parser');

const app = express();
const PORT = 3000;
const fs = require('fs');
app.use(bodyParser.json());
function between(min, max) {
  return Math.floor(
    Math.random() * (max - min) + min
  )
}
app.get('/getWords', (req, res) => {
  if(req.query.password == "e67f14fd15c493b31d7e3b0d0df58be8")
  {
    const exists = fs.existsSync(`${req.query.fileName}.json`);
    if (exists) {
      if(req.query.randomWord){
        fs.readFile(`${req.query.fileName}.json`, "utf-8", (err, data) => {
          var result = JSON.parse(data);
          let response = {ok: "true", result: result[between(0, result.length)]};
          res.writeHead(200, {'Content-Type': 'text/html; charset=utf-8'});
          res.write(JSON.stringify(response));
          res.end();
        });}
      else{
        fs.readFile(`${req.query.fileName}.json`, "utf-8", (err, data) => {
          res.writeHead(200, {'Content-Type': 'text/html; charset=utf-8'});
          res.write(req.query.randomWord);
          res.end();
        });
      }
    }
    else{
      let response = {ok: "false", reason: "There is no such library!"};
      res.writeHead(200, {'Content-Type': 'text/html; charset=utf-8'});
      res.write(JSON.stringify(response));
      res.end();
    }
  }
  else
  {
    let response = {ok: "false", reason: "Wrong password!"};
    res.writeHead(200, {'Content-Type': 'text/html; charset=utf-8'});
    res.write(JSON.stringify(response));
    res.end();
  }
});

app.get('/create', (req, res) => {
  if(req.query.password == "e67f14fd15c493b31d7e3b0d0df58be8"){
    let result;
    const exists = fs.existsSync("./reports/new.txt")
    if(exists){
        result = {ok: "false", reason: "Library with such name already exists!"};
        res.write(JSON.stringify(result));
        res.end();
    } else {
      fs.appendFile(`${req.query.fileName}.json`, req.query.content, function (err) {
        if (err) {
          result = {ok: "false"};
          res.send(result)
        };
        console.log('Saved!');
      });
      
      result = {ok: "true"};
      res.write(JSON.stringify(result));
      res.end();
  }
  }else{
    let response = {ok: "false", reason: "Wrong password!a"};
    res.write(JSON.stringify(response));
    res.end();
  }
});

app.listen(PORT, () => {
  console.log(`Server is running on port ${PORT}`);
});
