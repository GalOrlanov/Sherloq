using Amazon.Athena;
using Amazon.Athena.Model;
using Amazon.Runtime;
using Amazon.SecurityToken;
using Amazon.SecurityToken.Model;
using SqlQueryParsing.Connectors;
using SqlQueryParsing.DataAssets;
using System;
using SqlQueryParsing.DBConnectors;
using Microsoft.Build.Framework;
using Amazon;

namespace SqlQueryParsing
{
    class Program
    {
        public static void Main()
        {
            //DbAPI.WriteRawQueryData();

            var query = @"select * from table22 where (field36 like '17.%' or field36 like '15.%') and table22.field38 in (select field38 from table3 where table22.year=table3.year and table22.field38=table3.field38 and field22 ='לא נמצאה סיבה מתאימה עבור תוצאת חישוב זכאות, יש להבין מה גרם לתוצאת החישוב הזו');"; 

            var parsedQuery = SqlQueryParser.ParseQuery(query);
            var listOfTables = parsedQuery.listOfTables;
            var listOfFields = parsedQuery.listOfFields;
            var ast = parsedQuery.queryAST;

            foreach (Table table in listOfTables)
            {
                Console.WriteLine(table.ToString());
            }

            Console.WriteLine();

            foreach (Field field in listOfFields)
            {
                Console.WriteLine(field.ToString());
            }

            Console.WriteLine();
            Console.WriteLine(ast.ToString());

            //var accessKeyId = "AKIA4WM5RNV7Q4DZUGO4";
            //var secretAccessKey = "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";
            //AmazonAthenaClient amazonAthenaClient = new AmazonAthenaClient(accessKeyId, secretAccessKey, Amazon.RegionEndpoint.EUWest1);
            var dbAddress = "appsflyer.cabtgkt0rfzz.eu-west-1.rds.amazonaws.com";
            var dbRoleARN = "arn:aws:iam::872745627007:role/POC-Role-test";
            var port = 5432;
            var dbUser = "db_user";
            var region = RegionEndpoint.EUWest1;
            var externalId = "111";
            //var customerRoleARN = "arn:aws:iam::872745627007:role/POC-Role-test";
            //var externalId = "111";
            var customerRoleARN = "arn:aws:iam::195229424603:role/ext_sherloq_athena";
            var customerExternalId = "Sher2022";
            //var roleARN = "arn:aws:iam::185308078728:role/ext_sherloq_s3";
            //var externalId = "Sher2022";
            var dbConnection = new DBConnection(dbAddress, dbRoleARN, port, dbUser, region, externalId);

            var athenaConnector = new AWSAthenaConnector(customerRoleARN, customerExternalId, region);
            var latestQueries = athenaConnector.GetLatestQueries(dbConnection);
            //var tableName = "testUniqueKeys";
          
            //var count = 1;
            //var errorAmount = 0;

            //foreach (GetQueryExecutionResponse athenaQuery in latestQueries)
            //{
            //    try
            //    {
            //        //Console.WriteLine(athenaQuery);
            //        DbAPI.WriteRawQueryData(dbConnection, athenaQuery.QueryExecution, tableName);
            //        Console.WriteLine(count);
            //        count++;
            //        //var appsflyerQuery = SqlQueryParser.ParseQuery(athenaQuery.QueryExecution.Query);
            //        //Console.WriteLine($"{count} ================");
            //        //Console.WriteLine($"Query: {athenaQuery}");
            //        //foreach (Table table in appsflyerQuery.listOfTables)
            //        //{
            //        //    Console.WriteLine(table.ToString());
            //        //}

            //        //Console.WriteLine();

            //        //foreach (Field field in appsflyerQuery.listOfFields)
            //        //{
            //        //    Console.WriteLine(field.ToString());
            //        //}
            //        //Console.WriteLine("================");
            //        //count++;
            //    }
            //    catch (Exception ex)
            //    {
            //        errorAmount++;
            //    }
            //}
            //Console.WriteLine(errorAmount);
        }
    }
}