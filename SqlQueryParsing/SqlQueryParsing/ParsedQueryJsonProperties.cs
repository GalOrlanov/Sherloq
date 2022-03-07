using System.Collections.Generic;

namespace SqlQueryParsing
{
    class ParsedQueryJsonProperties
    {
        public List<string> FieldList { get; set; }
        public List<string> TableList { get; set; }
        public string Ast { get; set; }
    }
}
