using Amazon;
using Amazon.Athena;
using Amazon.Athena.Model;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.SecurityToken;
using Amazon.SecurityToken.Model;
using Npgsql;
using SqlQueryParsing.DBConnectors;
using System;
using System.Collections.Generic;
using System.IO;

namespace SqlQueryParsing.Connectors
{
    internal class AWSAthenaConnector
    {
        //private string roleARN;
        //private string externalId;
        //private string roleSessionName = "Sherloq";
        //private AWSCredentials credentials;
        private AmazonAthenaClient amazonAthenaClient;
        private Credentials credentials;
        private string roleARN;
        private string externalId;
        private RegionEndpoint region;

        internal AWSAthenaConnector(string roleARN, string externalId, RegionEndpoint region, string roleSessionName = "Sherloq")
        {
            this.roleARN = roleARN;
            this.externalId = externalId;
            this.region = region;
            RenewAWSAthenaClient();
        }

        internal List<GetQueryExecutionResponse> GetLatestQueries(DBConnection dbConnection, int maxResults = 50)
        {
            //AlonTest();
            bool stop = false;
            //string nextToken = File.ReadAllText(@"C:\repo\Sherloq\SqlQueryParsing\nextToken.txt");
            string nextToken = "";
            var listOfQueries = new List<GetQueryExecutionResponse>();
            ListQueryExecutionsResponse queryExecutionResponse = null;
            if (nextToken.Equals(""))
                queryExecutionResponse = this.amazonAthenaClient.ListQueryExecutions(new ListQueryExecutionsRequest() { MaxResults = maxResults });
            else
                queryExecutionResponse = this.amazonAthenaClient.ListQueryExecutions(new ListQueryExecutionsRequest() { MaxResults = maxResults, NextToken = nextToken });
            var count = 1;
            var errorAmount = 0;
            var tableName = "appsflyer_athena_rawdata";
            var amountOfDuplicates = 0;
           
            while (!stop)
            {

                foreach (string queryId in queryExecutionResponse.QueryExecutionIds)
                {
                    try
                    {
                        var queryResponse = this.amazonAthenaClient.GetQueryExecution(new GetQueryExecutionRequest()
                        {
                            QueryExecutionId = queryId
                        });

                        //listOfQueries.Add($"Query ID: {queryId}. Query: {queryResponse.QueryExecution.Query}");
                        //listOfQueries.Add(queryResponse.QueryExecution.Query);
                        //listOfQueries.Add(queryResponse);
                        dbConnection.OpenConnection();
                        DbAPI.WriteRawQueryData(dbConnection, queryResponse.QueryExecution, tableName);
                     
                        Console.WriteLine(count);
                        count++;
                    }
                    catch (PostgresException ex)
                    {
                        if (ex.Code.Equals("28000"))
                        {

                        }
                        else
                        {
                            amountOfDuplicates++;
                        }
                    }
                    catch (Exception ex)
                    {
                        errorAmount++;
                        RenewAWSAthenaClient();
                    }
                    finally
                    {
                        dbConnection.CloseConnection();
                    }
                }

                try
                {
                    if (nextToken == null)
                    {
                        stop = true;
                    }
                    else
                    {
                        nextToken = queryExecutionResponse.NextToken;
                        File.WriteAllText(@"C:\repo\Sherloq\SqlQueryParsing\nextToken.txt", nextToken);
                        queryExecutionResponse = this.amazonAthenaClient.ListQueryExecutions(new ListQueryExecutionsRequest() { MaxResults = 50, NextToken = nextToken });
                    }
                }
                catch(Exception ex)
                {
                    errorAmount++;
                    RenewAWSAthenaClient();
                }
            }

            Console.WriteLine(errorAmount);

            return listOfQueries;
        }

        private void RenewAWSAthenaClient()
        {
            this.credentials = AWSConnectorHelper.AssumeRole(this.roleARN, this.externalId);
            this.amazonAthenaClient = new AmazonAthenaClient(this.credentials, region);
        }

        private void AlonTest()
        {
            bool stop = false;
            string nextToken = "";
            var count = 1;
            var queryExecutionResponse = amazonAthenaClient.ListQueryExecutions(new ListQueryExecutionsRequest() { MaxResults = 50 });
            var path = @"C:\repo\Sherloq\SqlParserJS\alontest.txt";
            using (File.CreateText(path)){};
            while (!stop)
            {
                foreach (string queryId in queryExecutionResponse.QueryExecutionIds)
                {
                    using (StreamWriter sw = File.AppendText(path))
                    {
                        sw.WriteLine($"{count}: {queryId}");
                    }
     
                    count++;
                }

                if (nextToken == null)
                {
                    stop = true;
                }
                else
                {
                    nextToken = queryExecutionResponse.NextToken;

                    queryExecutionResponse = amazonAthenaClient.ListQueryExecutions(new ListQueryExecutionsRequest() { MaxResults = 50, NextToken = nextToken });
                }
            }
        }
    }
}