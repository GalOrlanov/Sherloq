using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using SqlQueryParsing.DataAssets;
using Newtonsoft.Json;
using System;
using SqlQueryParsing.Exceptions;
using SqlQueryParsing.DBConnectors;

namespace SqlQueryParsing
{
    internal class SqlQueryParser
    {
        internal static QueryParsingResult ParseQuery(DbConnection dbConnection, Query query, Database database)
        {
            try
            {
                var parsedQueryString = ParseQueryJSConnector(query.query);

                if (parsedQueryString.Equals(""))
                {
                    throw new Exception();
                }

                return ParsedStringtoQueryParsingResult(dbConnection, parsedQueryString, database, query);
            }
            catch (Exception ex)
            {
                throw new ParsingException("Empty parsing");
            }
        }

        private static string ParseQueryJSConnector(string query)
        {
            try
            {
                using (Process compiler = new Process())
                {
                    File.WriteAllText(@"C:\repo\Sherloq\SqlParserJS\query.sql", query);

                    compiler.StartInfo.FileName = @"C:\Program Files\nodejs\node.exe";
                    compiler.StartInfo.WorkingDirectory = @"C:\repo\Sherloq\SqlParserJS";
                    compiler.StartInfo.Arguments = "queryParser.js";
                    compiler.StartInfo.UseShellExecute = false;
                    compiler.StartInfo.RedirectStandardOutput = true;
                    compiler.Start();

                    var parsedQuery = compiler.StandardOutput.ReadToEnd();
                    compiler.WaitForExit();

                    if (parsedQuery.StartsWith("Error"))
                    {
                        return "";
                    }

                    return parsedQuery;
                }
            }
            catch(Exception error)
            {
                throw new ParsingException($"Failed to connect to SqlParserJS. Message: {error.Message}");
            }
        }

        private static QueryParsingResult ParsedStringtoQueryParsingResult(DbConnection dbConnection, string parsedQuery, Database database, Query query)
        {
            var parsedJson = JsonConvert.DeserializeObject<ParsedQueryJsonProperties>(parsedQuery);
            var listOfTables = GetListOfTables(dbConnection, parsedJson.TableList, database);
            var listOfFields = GetListOfFields(dbConnection, parsedJson.FieldList, listOfTables, query);
            var ast = GetASTasJson(parsedJson.Ast);

            return new QueryParsingResult()
            {
                listOfFields = listOfFields,
                listOfTables = listOfTables,
                queryAST = ast
            };
        }

        private static List<Table> GetListOfTables(DbConnection dbConnection, List<string> listOfStringsOfTables, Database database)
        {
            try
            {
                var resultListOfTables = new List<Table>();

                foreach (string tableStr in listOfStringsOfTables)
                {
                    var tableInfoList = tableStr.Replace("::", ":").Split(':');
                    var schemaName = tableInfoList[1];
                    var tableName = tableInfoList[2];
                    var schema = DbAPI.GetSchema(dbConnection, database.name, schemaName);

                    if (schema == null)
                    {
                        schema = DbAPI.WriteNewSchema(dbConnection, new Schema(schemaName, database));
                    }

                    var table = DbAPI.GetTable(dbConnection, database.name, schema.name, tableName);

                    resultListOfTables.Add(table);
                }

                return resultListOfTables;
            }
            catch(Exception error)
            {
                throw new ParsingException($"Failed to get list of tables from query. Message: {error.Message}");
            }
        }

        private static List<Field> GetListOfFields(DbConnection dbConnection, List<string> listOfStringsOfFields, List<Table> listOfTables, Query query)
        {
            try
            {
                var resultListOfFields = new List<Field>();

                foreach (string fieldStr in listOfStringsOfFields)
                {
                    var fieldInfoList = fieldStr.Replace("::", ":").Split(':');
                    var tableName = fieldInfoList[1];
                    var fieldName = fieldInfoList[2];

                    if (tableName == "null")
                    {
                        // To Do: find list of possible tables
                    }

                    Table table = listOfTables.Find(x => x.name.Equals(tableName));
                    var field = DbAPI.GetField(dbConnection, table.schema.database.name, table.schema.name, tableName, fieldName);

                    resultListOfFields.Add(field);
                }

                return resultListOfFields;
            }
            catch (Exception error)
            {
                throw new ParsingException($"Failed to get list of fields from query. Message: {error.Message}");
            }
        }

        private static JObject GetASTasJson(string queryTreeString)
        {
            try
            {
                queryTreeString = queryTreeString.Replace("\n", "");
                var isAstArray = queryTreeString.StartsWith("[") && queryTreeString.EndsWith("]");
                if (isAstArray)
                {
                    queryTreeString = queryTreeString.Substring(1, queryTreeString.Length - 2);
                }

                return JObject.Parse(queryTreeString);
            }
            catch (Exception error)
            {
                throw new ParsingException($"Failed to get AST from query. Message: {error.Message}");
            }
        }
    }
}