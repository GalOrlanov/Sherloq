const { Parser } = require('node-sql-parser');
const parser = new Parser();
const fs = require('fs');

try {
    const query = fs.readFileSync(__dirname + '/query.sql', 'utf8');
    const ast = parser.astify(query)
    const tableList = parser.tableList(query)
    const fieldList = parser.columnList(query)
    var jsonResult = {};
    jsonResult.fieldList = fieldList;
    jsonResult.tableList = tableList;
    jsonResult.ast = JSON.stringify(ast);
    //const jsonResult = "{\"fieldList\": \"" + fieldList + "\", \"tableList\": \"" + tableList + "\", \"ast\": \"" + JSON.stringify(ast) + "\"}"
    console.log(jsonResult)
}
catch(error){
    console.log("Error");
}