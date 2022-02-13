using Amazon;
using Amazon.Athena;
using Amazon.Athena.Model;
using Amazon.Runtime;
using Amazon.SecurityToken;
using Amazon.SecurityToken.Model;
using System.Collections.Generic;

namespace SqlQueryParsing.Connectors
{
    internal class AWSAthenaConnector
    {
        private string roleARN;
        private string externalId;
        private string roleSessionName = "Sherloq";
        private RegionEndpoint region;
        private AWSCredentials credentials;
        private AmazonAthenaClient amazonAthenaClient;

        internal AWSAthenaConnector(string roleARN, string externalId, RegionEndpoint region, string roleSessionName = "Sherloq")
        {
            this.roleARN = roleARN;
            this.externalId = externalId;
            this.region = region;
            AssumeRole();
            this.amazonAthenaClient = new AmazonAthenaClient(this.credentials, region);
        }

        internal List<string> GetLatestQueries(int maxResults = 50)
        {
            var listOfQueries = new List<string>();
            var queryExecutionResponse = amazonAthenaClient.ListQueryExecutions(new ListQueryExecutionsRequest() { MaxResults = maxResults });
            foreach (string queryId in queryExecutionResponse.QueryExecutionIds)
            {
                var queryResponse = amazonAthenaClient.GetQueryExecution(new GetQueryExecutionRequest()
                {
                    QueryExecutionId = queryId
                });

                listOfQueries.Add($"Query ID: {queryId}. Query: {queryResponse.QueryExecution.Query}");
            }

            return listOfQueries;
        }

        private void AssumeRole()
        {
            var role = new AssumeRoleRequest
            {
                RoleArn = this.roleARN,
                RoleSessionName = this.roleSessionName,
                ExternalId = this.externalId
            };
            var amazonSecurityTokenServiceClient = new AmazonSecurityTokenServiceClient(new AmazonSecurityTokenServiceConfig { MaxErrorRetry = 3 });
            var response = amazonSecurityTokenServiceClient.AssumeRole(role);

            this.credentials = response.Credentials;
        }
    }
}