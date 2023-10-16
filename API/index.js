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

function findPack(arr, filename){
  for(let i = 0; i < arr.length; i++){
    if(arr[i].filename == filename){
      console.log(i);
      return i;
    }
  }
}

function checkExists(arr, username){
  for(let i = 0; i < arr.length; i++){
    if(arr[i].username == username){
      return 1;
    }
  }
  return 0;
}

app.get('/getWords', (req, res) => {
    const exists = fs.existsSync(`./${req.query.fileName}.json`);
    if (exists) {
      fs.readFile(`${req.query.fileName}.json`, "utf-8", (err, data) => {
        
        let file = JSON.parse(data);
        fs.readFile(`IDs.json`, "utf-8", (err, id) => {
          let ids = JSON.parse(id);
          ids[findPack(ids, req.query.fileName)].usage += 1;
          fs.unlinkSync("./IDs.json");
          fs.appendFile(`./IDs.json`, JSON.stringify(ids), function (err) {
            if (err) {
              result = {ok: "false", reason: err};
              res.send(result)
            };
            console.log('ID addedd!');
          });
        });
        if(req.query.randomWord){
          file.words = JSON.parse(file.words);
          response = {ok: "true", result: file.words[between(0, file.words.length)]};
          console.log(file.words);
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
      response = {ok: "false", reason: "There is no such library!"};
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
    let leng = (req.query.length ? req.query.length : 0);
    let end = leng + 10;
    console.log(tmp);
    let arr = [];
    for(let i = 0; i < end && i < tmp.length; i++){
      arr[i] = tmp[i];
    }
    res.writeHead(200, {'Content-Type': 'charset=utf-8'});
    res.write(JSON.stringify(arr));
    res.end();
    // fs.readFile(`${req.query.fileName}.json`, "utf-8", (err, data) => {
    //   let file = JSON.parse(data);
    //   if(req.query.randomWord){
    //     response = {ok: "true", result: file[between(0, file.length)]};
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
    result;
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
          let Words = req.query.content;
          let Login = req.query.login;
          let Password = req.query.password;
          let content = {words: Words, login: Login, password: Password}
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
          file[file.length] = {filename: req.query.fileName, usage: 0};
          fs.unlinkSync("./IDs.json");
          fs.appendFile(`./IDs.json`, JSON.stringify(file), function (err) {
            if (err) {
              result = {ok: "false", reason: err};
              res.send(result)
            };
            console.log('ID addedd!');
          });
        });
      }else{
        let file = [];
        file[file.length] = {filename: req.query.fileName, usage: 0};
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
        let content = {words: Words, login: Login, password: Password}
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
    response = {ok: "false", reason: "You are not registered!"};
    res.write(JSON.stringify(response));
    res.end();
  }
});

app.get('/deletePack', (req, res) => {
  result;
  const exists = fs.existsSync(`./${req.query.fileName}.json`);
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
          console.log(ids[findPack(ids, req.query.fileName)]);
          let tmp = [];
          for(let i = 0, j = 0; i < ids.length; i++, j++){
            if(ids[i].filename != req.query.fileName){
              tmp[j] = ids[i];
            }else{
              i++;
            }
          }
          fs.unlink(`./IDs.json`, function (err) {
            if (err) {
              result = {ok: "false"};
              res.send(result)
            };
            console.log('Deleted! IDs');
          });
          fs.appendFile(`./IDs.json`, JSON.stringify(tmp), function (err) {
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
        response = {ok: "false", reason: "Wrong username or password!"};
        console.log(req.query.login + " " + req.query.password);
        console.log(file.login + " " + file.password);

        res.writeHead(200, {'Content-Type': 'charset=utf-8'});
        res.write(JSON.stringify(response));
        res.end();
      }
    });
  }
});

app.get('/addUser', (req, res) => {
  const exists = fs.existsSync(`./users.json`);
  var result = {};
  if(!exists){
    var usernameRegexPattern =/^(?=.*[a-zA-Z])(?=.*[0-9])[a-zA-Z0-9]+$/;
    if(usernameRegexPattern.test(req.query.username)){
      var passwordRegexPattern = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,20}$/;
      if(passwordRegexPattern.test(req.query.password)){
        if(req.query.password == req.query.confirm){
          let users = [{username: req.query.username, password: req.query.password}];
          fs.appendFile(`./users.json`, JSON.stringify(users), function (err) {
            if (err) {
              result = {ok: "false"};
              res.send(result)
            };
            console.log('User created addedd!');
          });
        }else{
          result = {ok: false, reason: "Passwords do not match!"};
        }
      }else{
      result = {ok: false, reason: "Password is not valid!"};
    }
    }else{
      result = {ok: false, reason: "Username is not valid!"};
    }
  }else{
    fs.readFile('IDs.json', "utf-8", (err, data) => {
      if(!checkExists(JSON.parse(data), req.query.username)){
        var usernameRegexPattern =/^(?=.*[a-zA-Z])(?=.*[0-9])[a-zA-Z0-9]+$/;
        if(usernameRegexPattern.test(req.query.username)){
          var passwordRegexPattern = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,20}$/;
          if(passwordRegexPattern.test(req.query.password)){
            if(req.query.password == req.query.confirm){
              let users = JSON.parse(data);
              users[users.length] = {username: req.query.username, password: req.query.password};
              fs.unlink(`./users.json`, function (err) {
                if (err) {
                  result = {ok: "false"};
                  res.send(result)
                };
                console.log('Deleted! users');
              });
              fs.appendFile(`./users.json`, JSON.stringify(users), function (err) {
                if (err) {
                  result = {ok: "false"};
                  res.send(result);
                };
                console.log('User created addedd!');
              });
            }else{
              result = {ok: false, reason: "Passwords do not match!"};
            }
          }
          else{
          result = {ok: false, reason: "Password is not valid!"};
          }
        }else{
          result = {ok: false, reason: "Username is not valid!"};
        }
      }else{
        result = {ok: false, reason: "Such username already exists!"};
      }
    });
    
  }
  res.write(result);
  res.end();
});

app.listen(PORT, () => {
  console.log(`Server is running on port ${PORT}`);
});
