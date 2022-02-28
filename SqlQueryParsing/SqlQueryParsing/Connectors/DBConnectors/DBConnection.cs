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
    internal class DBConnection
    {
        internal NpgsqlConnection connection;

        internal DBConnection(string dbAddress, string roleARN, int port, string db_user, RegionEndpoint region, string externalId)
        {
            //var credentials = AWSConnectorHelper.AssumeRole(roleARN, externalId);
            //var token = RDSAuthTokenGenerator.GenerateAuthToken(credentials, region, dbAddress, port, db_user);

            //var connectionString = $"Host={dbAddress};Username={db_user};Password={token};Database=postgres;SslMode=Prefer;port={port};";
            var connectionString = $"Host={dbAddress};Username=postgres;Password=Aip123456789;Database=postgres;SslMode=Prefer;port={port};";

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
