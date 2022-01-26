using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using SqlQueryParsing.DataAssets;

namespace SqlQueryParsing
{
    internal class SqlQueryParser
    {
        internal static QueryParsingResult ParseQuery(string query)
        {
            using (Process compiler = new Process())
            {
                File.WriteAllText(@"C:\Zell\Sherloq\SqlParserJS\query.sql", query);

                compiler.StartInfo.FileName = @"C:\Program Files\nodejs\node.exe";
                compiler.StartInfo.WorkingDirectory = @"C:\Zell\Sherloq\SqlParserJS";
                compiler.StartInfo.Arguments = "queryParser.js";
                compiler.StartInfo.UseShellExecute = false;
                compiler.StartInfo.RedirectStandardOutput = true;
                compiler.Start();

                var parsedQuery = compiler.StandardOutput.ReadToEnd();
                compiler.WaitForExit();

                //Console.WriteLine(parsedQuery);
                return ParsedStringtoQueryParsingResult(parsedQuery);
            }
        }

        private static List<Table> GetListOfTables(string strOfTables)
        {
            var listOfStrOfTables = strOfTables.Split(',');
            var listOfTables = new List<Table>();

            foreach (string tableStr in listOfStrOfTables)
            {
                var tableInfoList = tableStr.Replace("::", ":").Split(':');
                listOfTables.Add(new Table(tableInfoList[2]));
            }

            return listOfTables;
        }

        private static List<Field> GetListOfFields(string strOfFields, List<Table> listOfTables)
        {
            var listOfStrOfFields = strOfFields.Split(',');
            var listOfFields = new List<Field>();

            foreach (string fieldStr in listOfStrOfFields)
            {
                var fieldInfoList = fieldStr.Replace("::", ":").Split(':');
                Table table = listOfTables.Find(a => a.name.Equals(fieldInfoList[1]));
                listOfFields.Add(new Field(fieldInfoList[2], table));
            }

            return listOfFields;
        }

        private static JObject GetASTasJson(string queryTreeString)
        {
            queryTreeString = queryTreeString.Replace("\n", "");
            var a = queryTreeString.StartsWith("[");
            var b = queryTreeString.EndsWith("]");
            if (a && b)
            {
                queryTreeString = queryTreeString.Substring(1, queryTreeString.Length - 2);
            }
            return JObject.Parse(queryTreeString);
        }

        private static QueryParsingResult ParsedStringtoQueryParsingResult(string parsedQuery)
        {
            var arrayOfResult = parsedQuery.Split(';');
            var listOfTables = GetListOfTables(arrayOfResult[1]);
            var listOfFields = GetListOfFields(arrayOfResult[0], listOfTables);
            var ast = GetASTasJson(arrayOfResult[2]);

            return new QueryParsingResult()
            {
                listOfFields = listOfFields,
                listOfTables = listOfTables,
                queryAST = ast
            };
        }
    }
}