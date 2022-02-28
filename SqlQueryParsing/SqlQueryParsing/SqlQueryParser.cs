using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using SqlQueryParsing.DataAssets;
using Newtonsoft.Json;
using System;
using SqlQueryParsing.Exceptions;

namespace SqlQueryParsing
{
    internal class SqlQueryParser
    {
        internal static QueryParsingResult ParseQuery(string query)
        {
            try
            {
                var parsedQuery = ParseQueryJSConnector(query);

                if (parsedQuery.Equals(""))
                {
                    throw new Exception();
                }

                return ParsedStringtoQueryParsingResult(parsedQuery);
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

        private static List<Table> GetListOfTables(string[] arrayOfTables)
        {
            try
            {
                var listOfTables = new List<Table>();

                foreach (string tableStr in arrayOfTables)
                {
                    var tableInfoList = tableStr.Replace("::", ":").Split(':');
                    listOfTables.Add(new Table(tableInfoList[2]));
                }

                return listOfTables;
            }
            catch(Exception error)
            {
                throw new ParsingException($"Failed to get list of tables from query. Message: {error.Message}");
            }
        }

        private static List<Field> GetListOfFields(string[] arrayOfFields, List<Table> listOfTables)
        {
            try
            {
                var listOfFields = new List<Field>();

                foreach (string fieldStr in arrayOfFields)
                {
                    var fieldInfoList = fieldStr.Replace("::", ":").Split(':');
                    Table table = listOfTables.Find(a => a.name.Equals(fieldInfoList[1]));
                    listOfFields.Add(new Field(fieldInfoList[2], table));
                }

                return listOfFields;
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

        private static QueryParsingResult ParsedStringtoQueryParsingResult(string parsedQuery)
        {
            var parsedJson = JsonConvert.DeserializeObject<ParsedQueryJsonProperties>(parsedQuery);
            var listOfTables = GetListOfTables(parsedJson.TableList);
            var listOfFields = GetListOfFields(parsedJson.FieldList, listOfTables);
            var ast = GetASTasJson(parsedJson.Ast);

            return new QueryParsingResult()
            {
                listOfFields = listOfFields,
                listOfTables = listOfTables,
                queryAST = ast
            };
        }
    }
}