const express = require('express');
const bodyParser = require('body-parser');
const cors = require('cors');

const app = express();
const PORT = 3000;
const fs = require('fs');
app.use(bodyParser.json());
app.use(cors());
function between(min, max) {
  return Math.floor(
    Math.random() * (max - min) + min
  )
}

function checkId(arr, id){
  for(let i = 0; i < arr.length; i++){
    if(arr[i].id == id){
      return true;
    }
  }
  return false;
}

function findPack(arr, filename){
  for(let i = 0; i < arr.length; i++){
    if(arr[i].filename == filename){
      console.log(i);
      return i;
    }
  }
}

app.get('/getWords', (req, res) => {
    const exists = fs.existsSync(`./${req.query.fileName}.json`);
    if (exists) {
      fs.readFile(`${req.query.fileName}.json`, "utf-8", (err, data) => {
        
        let file = JSON.parse(data);
        fs.readFile(`IDs.json`, "utf-8", (err, id) => {
          let ids = JSON.parse(id);
          ids[findPack(ids, req.query.fileName)].usage += 1;
        });
        if(req.query.randomWord){
          let response = {ok: "true", result: file[between(0, file.length)]};
          res.writeHead(200, {'Content-Type': 'charset=utf-8'});
          res.write(JSON.stringify(response));
          res.end();
        }
        else{
          res.writeHead(200, {'Content-Type': 'charset=utf-8'});
          res.write(file.words);
          res.end();
        }
      });
    }
    else{
      let response = {ok: "false", reason: "There is no such library!"};
      res.writeHead(200, {'Content-Type': 'charset=utf-8'});
      res.write(JSON.stringify(response));
      res.end();
    }
});

app.get('/getTopPacks', (req, res) => {
  fs.readFile(`IDs.json`, "utf-8", (err, id) => {
    let tmp = JSON.parse(id).sort((a, b) => {
      if (a.usage > b.usage) {
        return -1;
      }
    });
    let arr = (req.query.arr ? JSON.parse(req.query.arr) : []);
    let end = arr.length + 10;
    console.log(tmp);
    for(let i = arr.length; i < end && i < tmp.length; i++){
      arr[i] = tmp[i];
    }
    res.writeHead(200, {'Content-Type': 'charset=utf-8'});
    res.write(JSON.stringify(arr));
    res.end();
    // fs.readFile(`${req.query.fileName}.json`, "utf-8", (err, data) => {
    //   let file = JSON.parse(data);
    //   if(req.query.randomWord){
    //     let response = {ok: "true", result: file[between(0, file.length)]};
    //     res.writeHead(200, {'Content-Type': 'charset=utf-8'});
    //     res.write(JSON.stringify(response));
    //     res.end();
    //   } else{
    //     res.writeHead(200, {'Content-Type': 'charset=utf-8'});
    //     res.write(file.words);
    //     res.end();
    //   }
    // });
  });
});

app.get('/createPack', (req, res) => {
  if(req.query.isLogged){
    let result;
    const exists = fs.existsSync(`./${req.query.fileName}.json`);
    console.log(exists);
    if(exists){
        result = {ok: "false", reason: "Library with such name already exists!"};
        res.write(JSON.stringify(result));
        res.end();
    } 
    else {
      const exists2 = fs.existsSync(`./IDs.json`);
      if(exists2){
        fs.readFile('IDs.json', "utf-8", (err, data) => {
          let file = JSON.parse(data);
          if(!checkId(file, req.query.id)){
            let ID = req.query.id;
            let Words = req.query.content;
            let Login = req.query.login;
            let Password = req.query.password;
            let content = {id: ID, words: Words, login: Login, password: Password}
            fs.appendFile(`./${req.query.fileName}.json`, JSON.stringify(content), function (err) {
              if (err) {
                result = {ok: "false", reason: err};
                res.send(result)
              };
              console.log('Saved!' + content);
            });
            result = {ok: "true"};
            res.write(JSON.stringify(result));
            res.end();
            file[file.length] = {id: req.query.id, filename: req.query.fileName, usage: 0};
            fs.unlinkSync("./IDs.json");
            fs.appendFile(`./IDs.json`, JSON.stringify(file), function (err) {
              if (err) {
                result = {ok: "false", reason: err};
                res.send(result)
              };
              console.log('ID addedd!');
            });
          }
          else{
            result = {ok: "false", reason: "There is already library with such an ID!"};
            res.write(JSON.stringify(result));
            res.end();
          }
        });
      }else{
        let file = [];
        let ID = req.query.id;
        file[file.length] = {id: req.query.id, filename: req.query.fileName, usage: 0};
        fs.appendFile(`./IDs.json`, JSON.stringify(file), function (err) {
          if (err) {
            result = {ok: "false"};
            res.send(result)
          };
          console.log('ID addedd!');
        });
        let Words = req.query.content;
        let Login = req.query.login;
        let Password = req.query.password;
        let content = {id: ID, words: Words, login: Login, password: Password}
        fs.appendFile(`./${req.query.fileName}.json`, JSON.stringify(content), function (err) {
          if (err) {
            result = {ok: "false"};
            res.send(result)
          };
          console.log('Saved!' + content);
        });
        result = {ok: "true"};
        res.write(JSON.stringify(result));
        res.end();
      }
    }
  }
  else{
    let response = {ok: "false", reason: "You are not registered!"};
    res.write(JSON.stringify(response));
    res.end();
  }
});

app.get('/deletePack', (req, res) => {
  let result;
  const exists = fs.existsSync(`./${req.query.fileName}.json`)
  if(!exists){
      result = {ok: "false", reason: "Library with such name does not exist!"};
      res.write(JSON.stringify(result));
      res.end();
  } 
  else {
    fs.readFile(`./${req.query.fileName}.json`, "utf-8", (err, data) => {
      let file = JSON.parse(data);
      if(req.query.login == file.login && req.query.password == file.password)
      {
        fs.unlink(`./${req.query.fileName}.json`, function (err) {
          if (err) {
            result = {ok: "false"};
            res.send(result)
          };
          console.log('Deleted! ' + req.query.fileName);
        });
        
        fs.readFile('IDs.json', "utf-8", (err, data) => {
          let ids = JSON.parse(data);
          console.log(ids[file.id]);
          console.log(file.id);
          delete ids[file.id];
          fs.unlink(`./IDs.json`, function (err) {
            if (err) {
              result = {ok: "false"};
              res.send(result)
            };
            console.log('Deleted! IDs');
          });
          fs.appendFile(`./IDs.json`, JSON.stringify(ids), function (err) {
            if (err) {
              result = {ok: "false"};
              res.send(result)
            };
            console.log('ID addedd!');
          });
        });
        result = {ok: "true"};
        res.write(JSON.stringify(result));
        res.end();
      }
      else
      {
        let response = {ok: "false", reason: "Wrong username or password!"};
        console.log(req.query.login + " " + req.query.password);
        console.log(file.login + " " + file.password);

        res.writeHead(200, {'Content-Type': 'charset=utf-8'});
        res.write(JSON.stringify(response));
        res.end();
      }
    });
  }
});
app.listen(PORT, () => {
  console.log(`Server is running on port ${PORT}`);
});
