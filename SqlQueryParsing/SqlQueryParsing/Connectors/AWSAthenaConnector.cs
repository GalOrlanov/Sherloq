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

        internal List<GetQueryExecutionResponse> GetAndStoreAllLatestQueries(DbConnection dbConnection, int maxResults = 50)
        {
            bool stop = false;
            string nextToken = "";
            var listOfQueries = new List<GetQueryExecutionResponse>();
            ListQueryExecutionsResponse queryExecutionResponse = null;
            if (nextToken.Equals(""))
                queryExecutionResponse = this.amazonAthenaClient.ListQueryExecutions(new ListQueryExecutionsRequest() { MaxResults = maxResults });
            else
                queryExecutionResponse = this.amazonAthenaClient.ListQueryExecutions(new ListQueryExecutionsRequest() { MaxResults = maxResults, NextToken = nextToken });
      
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

                        dbConnection.OpenConnection();
                        DbAPI.WriteRawQueryData(dbConnection, queryResponse.QueryExecution);
                    }
                    catch (PostgresException ex)
                    {
                        if (ex.Code.Equals("28000"))
                        {

                        }
                        else
                        {
                      
                        }
                    }
                    catch (Exception ex)
                    {
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
                    RenewAWSAthenaClient();
                }
            }

            return listOfQueries;
        }

        private void RenewAWSAthenaClient()
        {
            this.credentials = AWSConnectorHelper.AssumeRole(this.roleARN, this.externalId);
            this.amazonAthenaClient = new AmazonAthenaClient(this.credentials, region);
        }
    }
}