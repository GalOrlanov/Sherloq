using Amazon;
using Amazon.RDS.Util;
using Npgsql;
using SqlQueryParsing.Connectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlQueryParsing.DBConnectors
{
    internal class DbConnection
    {
        internal NpgsqlConnection connection;

        internal DbConnection(string dbAddress, string roleARN, int port, string dbUser, RegionEndpoint region, string externalId)
        {
            var credentials = AWSConnectorHelper.AssumeRole(roleARN, externalId);
            var token = RDSAuthTokenGenerator.GenerateAuthToken(credentials, region, dbAddress, port, dbUser);

            var connectionString = $"Host={dbAddress};Username={dbUser};Password={token};Database=postgres;SslMode=Prefer;port={port};";

            this.connection = new NpgsqlConnection(connectionString);
        }

        internal void OpenConnection()
        {
            this.connection.Open();
        }

        internal void CloseConnection()
        {
            this.connection.Close();
        }
    }
}
