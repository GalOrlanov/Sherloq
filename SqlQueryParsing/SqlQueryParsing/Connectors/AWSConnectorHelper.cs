using Amazon.SecurityToken;
using Amazon.SecurityToken.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlQueryParsing.Connectors
{
    internal class AWSConnectorHelper
    {
        internal static Credentials AssumeRole(string roleARN, string externalId, string roleSessionName = "Sherloq")
        {
            var role = new AssumeRoleRequest
            {
                RoleArn = roleARN,
                RoleSessionName = roleSessionName,
                ExternalId = externalId,
                DurationSeconds = 3600
            };
            var amazonSecurityTokenServiceClient = new AmazonSecurityTokenServiceClient(new AmazonSecurityTokenServiceConfig { MaxErrorRetry = 3 });
            var response = amazonSecurityTokenServiceClient.AssumeRole(role);

            return response.Credentials;
        }
    }
}
