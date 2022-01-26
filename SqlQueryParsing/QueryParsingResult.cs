using Newtonsoft.Json.Linq;
using SqlQueryParsing.DataAssets;
using System.Collections.Generic;

namespace SqlQueryParsing
{
    internal class QueryParsingResult
    {
        internal List<Field> listOfFields { get; set; }
        internal List<Table> listOfTables { get; set; }
        internal JObject queryAST { get; set; }
    }
}
