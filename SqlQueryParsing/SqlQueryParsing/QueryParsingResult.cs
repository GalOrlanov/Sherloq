using Newtonsoft.Json.Linq;
using SqlQueryParsing.DataAssets;
using System.Collections.Generic;

namespace SqlQueryParsing
{
    internal class QueryParsingResult
    {
        internal List<string> listOfFields { get; set; } // DB.table.field
        internal List<string> listOfJoins { get; set; }
        internal List<Table> listOfTables { get; set; } // will not use probably
        internal JObject queryAST { get; set; }
    }
}
