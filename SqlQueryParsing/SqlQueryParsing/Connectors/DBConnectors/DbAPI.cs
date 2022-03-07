using Amazon.Athena.Model;
using Npgsql;
using SqlQueryParsing.DataAssets;
using System;

using Database = SqlQueryParsing.DataAssets.Database;

namespace SqlQueryParsing.DBConnectors
{
    internal class DbAPI
    {
        internal static void WriteRawQueryData(DbConnection dbConnection, QueryExecution queryExecution, string tableName) 
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

            var cmd = new NpgsqlCommand(sql, dbConnection.connection);
    
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

        internal static void WriteNewDatabase(DbConnection dbConnection, Database database)
        {
            dbConnection.OpenConnection();
            var sql = $"INSERT INTO {ClientResources.AppsFlyerSchemaName}.{ClientResources.DatabasesTableName} (db_name, db_description) VALUES(@db_name, @db_description)";
            var cmd = new NpgsqlCommand(sql, dbConnection.connection);
            cmd.Parameters.AddWithValue("@db_name", database.name);
            cmd.Parameters.AddWithValue("@db_description", database.description);
            cmd.ExecuteNonQuery();
            dbConnection.CloseConnection();
        }

        internal static void WriteNewSchema(DbConnection dbConnection, Schema schema)
        {
            dbConnection.OpenConnection();
            var sql = $"INSERT INTO {ClientResources.AppsFlyerSchemaName}.{ClientResources.SchemasTableName} (schema_name, schema_description, dbId) VALUES(@schema_name, @schema_description, @dbId)";
            var cmd = new NpgsqlCommand(sql, dbConnection.connection);
            cmd.Parameters.AddWithValue("@schema_name", schema.name);
            cmd.Parameters.AddWithValue("@schema_description", schema.description);
            cmd.Parameters.AddWithValue("@dbId", schema.database.dbId);
            cmd.ExecuteNonQuery();
            dbConnection.CloseConnection();
        }

        internal static void WriteNewTable(DbConnection dbConnection, Table table)
        {
            dbConnection.OpenConnection();
            var sql = $"INSERT INTO {ClientResources.AppsFlyerSchemaName}.{ClientResources.TablesTableName} (table_name, table_description, schemaId, dbId) VALUES(@table_name, @table_description, @schemaId, @dbId)";
            var cmd = new NpgsqlCommand(sql, dbConnection.connection);
            cmd.Parameters.AddWithValue("@table_name", table.name);
            cmd.Parameters.AddWithValue("@table_description", table.description);
            cmd.Parameters.AddWithValue("@schemaId", table.schema.schemaId);
            cmd.Parameters.AddWithValue("@dbId", table.schema.database.dbId);
            cmd.ExecuteNonQuery();
            dbConnection.CloseConnection();
        }

        internal static void WriteNewField(DbConnection dbConnection, Field field)
        {
            dbConnection.OpenConnection();
            var sql = $"INSERT INTO {ClientResources.AppsFlyerSchemaName}.{ClientResources.FieldsTableName} (field_name, field_description, field_type, tableId, schemaId, dbId) VALUES(@field_name, @field_description, @field_type, @tableId, @schemaId, @dbId)";
            var cmd = new NpgsqlCommand(sql, dbConnection.connection);
            cmd.Parameters.AddWithValue("@field_name", field.name);
            cmd.Parameters.AddWithValue("@field_description", field.description);
            cmd.Parameters.AddWithValue("@field_type", field.fieldType.ToString());
            cmd.Parameters.AddWithValue("@tableId", field.table.tableId);
            cmd.Parameters.AddWithValue("@schemaId", field.table.schema.schemaId);
            cmd.Parameters.AddWithValue("@dbId", field.table.schema.database.dbId);
            cmd.ExecuteNonQuery();
            dbConnection.CloseConnection();
        }

        internal static void writeNewQuery(DbConnection dbConnection, Query query)
        {
            dbConnection.OpenConnection();
            var sql = $"INSERT INTO {ClientResources.AppsFlyerSchemaName}.{ClientResources.QueriesTableName} (query, query_description, query_name) VALUES(@query, @query_description, @query_name)";
            var cmd = new NpgsqlCommand(sql, dbConnection.connection);
            cmd.Parameters.AddWithValue("@query", query.query);
            cmd.Parameters.AddWithValue("@query_description", query.description);
            cmd.Parameters.AddWithValue("@query_name", query.name);
            cmd.ExecuteNonQuery();
            dbConnection.CloseConnection();
        }

        internal static void WriteFieldToAccessLog(DbConnection dbConnection, Field field, Query query, DateTime querySubmissionDateTime)
        {
            dbConnection.OpenConnection();
            var sql = $"INSERT INTO {ClientResources.AppsFlyerSchemaName}.{ClientResources.ParsedQueriesFieldAccessLog} (fieldId, tableId, schemaId, dbId, queryId sherloq_query_execution_id, query_submission_datetime) VALUES(@fieldId, @tableId, @schemaId, @dbId, @queryId, @sherloq_query_execution_id, @query_submission_datetime)";
            var cmd = new NpgsqlCommand(sql, dbConnection.connection);
            cmd.Parameters.AddWithValue("@fieldId", field.fieldId);
            cmd.Parameters.AddWithValue("@tableId", field.table.tableId);
            cmd.Parameters.AddWithValue("@schemaId", field.table.schema.schemaId);
            cmd.Parameters.AddWithValue("@dbId", field.table.schema.database.dbId);
            cmd.Parameters.AddWithValue("@queryId", query.queryId);
            cmd.Parameters.AddWithValue("@sherloq_query_execution_id", "");
            cmd.Parameters.AddWithValue("@query_submission_datetime", querySubmissionDateTime);
            cmd.ExecuteNonQuery();
            dbConnection.CloseConnection();
        }

        //Get db from db or null if db doesn't exist
        internal static Database GetDb(DbConnection dbConnection, string dbName)
        {
            dbConnection.OpenConnection();
            var sql = $"SELECT * FROM {ClientResources.AppsFlyerSchemaName}.{ClientResources.DatabasesTableName} WHERE db_name = @db_name";
            Database db = null;

            using (NpgsqlCommand cmd = new NpgsqlCommand(sql, dbConnection.connection))
            {
                cmd.Parameters.AddWithValue("@db_name", dbName);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                reader.Read();

                if (!reader.HasRows)
                {
                    dbConnection.CloseConnection();
                    return null;
                }

                var dbId = Int32.Parse(reader["dbId"].ToString());
                var dbDescription = reader["db_description"].ToString();
                db = new Database(dbName, dbId, dbDescription);
            }
            dbConnection.CloseConnection();

            return db;
        }

        //Get schema from db or null if schema doesn't exist
        internal static Schema GetSchema(DbConnection dbConnection, string dbName, string schemaName)
        {
            var db = GetDb(dbConnection, dbName);
            var sql = $"SELECT * FROM {ClientResources.AppsFlyerSchemaName}.{ClientResources.SchemasTableName} WHERE schema_name = @schema_name AND dbId = @dbId";
            Schema schema = null;

            dbConnection.OpenConnection();
            using (NpgsqlCommand cmd = new NpgsqlCommand(sql, dbConnection.connection))
            {
                cmd.Parameters.AddWithValue("@schema_name", schemaName);
                cmd.Parameters.AddWithValue("@dbId", db.dbId);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                reader.Read();

                if (!reader.HasRows)
                {
                    dbConnection.CloseConnection();
                    return null;
                }

                var schemaId = Int32.Parse(reader["schemaId"].ToString());
                var schemaDescription = reader["schema_description"].ToString();
                schema = new Schema(schemaName, db, schemaId , schemaDescription);
            }
            dbConnection.CloseConnection();

            return schema;
        }

        //Get table from db or null if table doesn't exist
        internal static Table GetTable(DbConnection dbConnection, string dbName, string schemaName, string tableName)
        {
            var schema = GetSchema(dbConnection, dbName, schemaName);
            var sql = $"SELECT * FROM {ClientResources.AppsFlyerSchemaName}.{ClientResources.TablesTableName} WHERE table_name = @table_name AND schemaId = @schemaId";
            Table table = null;

            dbConnection.OpenConnection();
            using (NpgsqlCommand cmd = new NpgsqlCommand(sql, dbConnection.connection))
            {
                cmd.Parameters.AddWithValue("@table_name", tableName);
                cmd.Parameters.AddWithValue("@schemaId", schema.schemaId);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                reader.Read();

                if (!reader.HasRows)
                {
                    dbConnection.CloseConnection();
                    return null;
                }

                var tableId = Int32.Parse(reader["tableId"].ToString());
                var tableDescription = reader["table_description"].ToString();
                table = new Table(tableName, schema, tableId, tableDescription);
            }
            dbConnection.CloseConnection();

            return table;
        }

        //Get field from db or null if field doesn't exist
        internal static Field GetField(DbConnection dbConnection, string dbName, string schemaName, string tableName, string fieldName)
        {
            var table = GetTable(dbConnection, dbName, schemaName, tableName);
            var sql = $"SELECT * FROM {ClientResources.AppsFlyerSchemaName}.{ClientResources.FieldsTableName} WHERE field_name = @field_name AND tableId = @tableId";
            Field field = null;

            dbConnection.OpenConnection();
            using (NpgsqlCommand cmd = new NpgsqlCommand(sql, dbConnection.connection))
            {
                cmd.Parameters.AddWithValue("@field_name", fieldName);
                cmd.Parameters.AddWithValue("@tableId", table.tableId);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                reader.Read();

                if (!reader.HasRows)
                {
                    dbConnection.CloseConnection();
                    return null;
                }

                var fieldId = Int32.Parse(reader["fieldId"].ToString());
                var fieldType = reader["field_type"].ToString();
                var fieldDescription = reader["field_description"].ToString();
                field = new Field(fieldName, table, fieldType, fieldId, fieldDescription);
            }
            dbConnection.CloseConnection();

            return field;
        }

        //Get query from db or null if query doesn't exist
        internal static Query GetQuery(DbConnection dbConnection, string queryString)
        {
            var sql = $"SELECT * FROM {ClientResources.AppsFlyerSchemaName}.{ClientResources.QueriesTableName} WHERE query = @query";
            Query query = null;

            dbConnection.OpenConnection();
            using (NpgsqlCommand cmd = new NpgsqlCommand(sql, dbConnection.connection))
            {
                cmd.Parameters.AddWithValue("@query", queryString);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                reader.Read();

                if (!reader.HasRows)
                {
                    dbConnection.CloseConnection();
                    return null;
                }

                var queryId = Int32.Parse(reader["queryId"].ToString());
                var queryDescription = reader["query_description"].ToString();
                var queryName = reader["query_name"].ToString();
                var sherloqQueryExecutions = Convert.IsDBNull(reader["sherloq_query_executions"]) ? null : (int[])reader["sherloq_query_executions"];
                query = new Query(queryString, queryName, queryDescription, queryId, sherloqQueryExecutions);
            }
            dbConnection.CloseConnection();

            return query;
        }

        internal static void AddQueryExecutionIdToQuery(DbConnection dbConnection, Query query, int sherloqQueryExecutionId)
        {
            var sql = $"UPDATE {ClientResources.AppsFlyerSchemaName}.{ClientResources.QueriesTableName} SET sherloq_query_executions = array_append(sherloq_query_executions, @sherloq_query_execution_id) WHERE query = @query";
            
            dbConnection.OpenConnection();
            using (NpgsqlCommand cmd = new NpgsqlCommand(sql, dbConnection.connection))
            {
                cmd.Parameters.AddWithValue("@query", query.query);
                cmd.Parameters.AddWithValue("@sherloq_query_execution_id", sherloqQueryExecutionId);
                cmd.ExecuteNonQuery();
            }
            dbConnection.CloseConnection();
        }
    }
}