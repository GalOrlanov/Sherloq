const { Parser } = require('node-sql-parser');
const parser = new Parser();
const fs = require('fs');

const query = fs.readFileSync(__dirname + '/query.sql', 'utf8');
const ast = parser.astify(query)
const tableList = parser.tableList(query)
const fieldList = parser.columnList(query)

console.log(fieldList + ";" + tableList + ";" + JSON.stringify(ast))