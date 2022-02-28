using Amazon;
using Amazon.Athena.Model;
using Amazon.RDS.Util;
using Amazon.SecurityToken;
using Amazon.SecurityToken.Model;
using Npgsql;
using System;

namespace SqlQueryParsing.DBConnectors
{
    internal class DbAPI
    {
        internal static void WriteRawQueryData(DBConnection dBConnection, QueryExecution queryExecution, string tableName) 
        {
            var queryExecutionId = Guid.Parse(queryExecution.QueryExecutionId);
            var query = queryExecution.Query;
            var querySubmissionDateTime = queryExecution.Status.SubmissionDateTime;
            var queryCompletionDateTime = queryExecution.Status.CompletionDateTime;
            var catalog = queryExecution.QueryExecutionContext.Catalog ?? "";
            var database = queryExecution.QueryExecutionContext.Database ?? "";
            var encryptionConfigurationKmsKey = (queryExecution.ResultConfiguration.EncryptionConfiguration != null) ? queryExecution.ResultConfiguration.EncryptionConfiguration.KmsKey : "";
            var encryptionConfigurationEncryptionOption = (queryExecution.ResultConfiguration.EncryptionConfiguration != null) ? queryExecution.ResultConfiguration.EncryptionConfiguration.EncryptionOption.Value : "";
            var outputLocation = queryExecution.ResultConfiguration.OutputLocation;
            var statementType = (queryExecution.StatementType != null) ? queryExecution.StatementType.Value : "";
            var dataManifestLocation = queryExecution.Statistics.DataManifestLocation ?? "";
            var dataScannedInBytes = queryExecution.Statistics.DataScannedInBytes;
            var engineExecutionTimeInMillis = queryExecution.Statistics.EngineExecutionTimeInMillis;
            var queryPlanningTimeInMillis = queryExecution.Statistics.QueryPlanningTimeInMillis;
            var queryQueueTimeInMillis = queryExecution.Statistics.QueryQueueTimeInMillis;
            var serviceProcessingTimeInMillis = queryExecution.Statistics.ServiceProcessingTimeInMillis;
            var totalExecutionTimeInMillis = queryExecution.Statistics.TotalExecutionTimeInMillis;
            var athenaError = (queryExecution.Status.AthenaError != null) ? queryExecution.Status.AthenaError.ErrorCategory : 0;
            var returnStateOfQueryRun = queryExecution.Status.State.Value;
            var returnStateChangeReason = queryExecution.Status.StateChangeReason ?? "";
            var workGroup = queryExecution.WorkGroup ?? "";


            var sql = $"INSERT INTO {tableName} VALUES(@queryExecutionId, @query, @querySubmissionDateTime, @queryCompletionDateTime, @catalog, " +
                $"@database, @encryptionConfigurationKmsKey, @encryptionConfigurationEncryptionOption, @outputLocation, @statementType, " +
                $"@dataManifestLocation, @dataScannedInBytes, @engineExecutionTimeInMillis, @queryPlanningTimeInMillis, @queryQueueTimeInMillis, " +
                $"@serviceProcessingTimeInMillis, @totalExecutionTimeInMillis, @athenaError, @returnStateOfQueryRun, @returnStateChangeReason, @workGroup);";

            var cmd = new NpgsqlCommand(sql, dBConnection.connection);
    
            cmd.Parameters.AddWithValue("@queryExecutionId", queryExecutionId);
            cmd.Parameters.AddWithValue("@query", query);
            cmd.Parameters.AddWithValue("@catalog", catalog);
            cmd.Parameters.AddWithValue("@database", database);
            cmd.Parameters.AddWithValue("@encryptionConfigurationKmsKey", encryptionConfigurationKmsKey);
            cmd.Parameters.AddWithValue("@encryptionConfigurationEncryptionOption", encryptionConfigurationEncryptionOption);
            cmd.Parameters.AddWithValue("@outputLocation", outputLocation);
            cmd.Parameters.AddWithValue("@statementType", statementType);
            cmd.Parameters.AddWithValue("@dataManifestLocation", dataManifestLocation);
            cmd.Parameters.AddWithValue("@dataScannedInBytes", dataScannedInBytes);
            cmd.Parameters.AddWithValue("@engineExecutionTimeInMillis", engineExecutionTimeInMillis);
            cmd.Parameters.AddWithValue("@queryPlanningTimeInMillis", queryPlanningTimeInMillis);
            cmd.Parameters.AddWithValue("@queryQueueTimeInMillis", queryQueueTimeInMillis);
            cmd.Parameters.AddWithValue("@serviceProcessingTimeInMillis", serviceProcessingTimeInMillis);
            cmd.Parameters.AddWithValue("@totalExecutionTimeInMillis", totalExecutionTimeInMillis);
            cmd.Parameters.AddWithValue("@athenaError", athenaError);
            cmd.Parameters.AddWithValue("@returnStateOfQueryRun", returnStateOfQueryRun);
            cmd.Parameters.AddWithValue("@returnStateChangeReason", returnStateChangeReason);
            cmd.Parameters.AddWithValue("@querySubmissionDateTime", querySubmissionDateTime);
            cmd.Parameters.AddWithValue("@queryCompletionDateTime", queryCompletionDateTime);
            cmd.Parameters.AddWithValue("@workGroup", workGroup);
            cmd.ExecuteNonQuery();
        }
    }
}
