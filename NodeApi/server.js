const express = require('express');
const bodyParser = require('body-parser');

const app = express();
app.use(bodyParser.urlencoded({ extended: true }));
const port = 8084;

let users = {
    1: { id: 1, firstName: 'Ola', lastName: 'Nordmann' },
    2: { id: 2, firstName: 'Kari', lastName: 'Nordkvinne' },
};

const newId = () => {
    return Math.max(...Object.keys(users)) + 1;
}

app.get('/users', (req, res) => {
    console.log("GET");
    return res.send(Object.values(users));
});

app.get('/users/:userId', (req, res) => {
    console.log("GET", req.params.userId);
    return res.send(users[req.params.userId]);
});

app.post('/user', (req, res) => {
    console.log("POST", req.body);
    const id = newId();
    const newUser = { id, ...req.body }

    users[id] = newUser;
    return res.send(newUser);
});

app.put('/user/:userId', (req, res) => {
    const id = req.params.userId;
    console.log("PUT", {id, body: req.body});

    users[id] = {...users[id], ...req.body};

    return res.send(users[id]);
});
  
app.delete('/user/:userId', (req, res) => {
    const id = req.params.userId;
    console.log("DELETE", id);

    delete users[id];

    return res.send();
  });

app.listen(port, () => {
    console.log('We are live on ' + port);
});