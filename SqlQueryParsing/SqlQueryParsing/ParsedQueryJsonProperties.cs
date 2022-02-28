namespace SqlQueryParsing
{
    class ParsedQueryJsonProperties
    {
        public string[] FieldList { get; set; }
        public string[] TableList { get; set; }
        public string Ast { get; set; }
    }
}
