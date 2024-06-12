const EventEmmiter = require('events');
const emitter = new EventEmmiter();
let array = [];
console.log(array);
emitter.on('load', (datas) => {
    array.push(...datas);
});
emitter.emit('load', [1, 2, 3, 4, 5, 6, 7, 8, 9, 10]);
console.log(array);