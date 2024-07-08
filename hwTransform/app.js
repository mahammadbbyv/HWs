//Создать массив объектов где в некоторых ключах есть null. Через Transform отфильтровать массив на null и показать отфильтрованные данные в консоли
const { Transform } = require('stream');

const arr = [
    { key: null, value: 'value1' },
    { key: 1, value: 'value2' },
    { key: 2, value: 'value3' },
    { key: null, value: 'value4' },
    { key: 3, value: 'value5' }
];

const filterNull = new Transform({
    objectMode: true,
    transform(chunk, encoding, callback) {
        if (chunk.key !== null) {
            this.push(chunk);
        }
        callback();
    }
});

arr.forEach((item) => {
    filterNull.write(item);
});

filterNull.end();

filterNull.on('data', (chunk) => {
    console.log(chunk);
});
