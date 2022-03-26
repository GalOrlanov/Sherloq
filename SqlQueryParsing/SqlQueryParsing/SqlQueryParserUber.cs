using System;
namespace SqlQueryParsing
{
    public class SqlQueryParserUber
    {
        

        public static string ParseStringToQuery(string s)
        {
            string query = "result";
            return query;
        }

        public static string SendRequestToUberParser(string s)
        {
            // mashu ParseStringToQuery(s)
        }

        public static QueryParsingResult GetResultParser(string s)
        {
            string resultFromUber = SendRequestToUberParser(s)


        }

        public SqlQueryParserUber()
        {

        }
    }
}




// input: String ->
// """"""" ' 

/*
 * 
 * curl -X POST http://52.214.88.62:3000/parser \
                                             -H 'Content-Type: application/json' \
                                             -d '{"query": "${query}"}'
 */