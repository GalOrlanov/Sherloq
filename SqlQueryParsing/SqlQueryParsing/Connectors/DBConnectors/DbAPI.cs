using Amazon.Athena.Model;
using Npgsql;
using SqlQueryParsing.DataAssets;
using SqlQueryParsing.Exceptions;
using System;
using Database = SqlQueryParsing.DataAssets.Database;

namespace SqlQueryParsing.DBConnectors
{
    internal class DbAPI
    {
        internal static int WriteRawQueryData(DbConnection dbConnection, QueryExecution queryExecution) 
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


            var sql = $"INSERT INTO {ClientResources.AppsFlyerSchemaName}.{ClientResources.AthenaRawDataQueryHistoryTableName} VALUES(@queryExecutionId, @query, @querySubmissionDateTime, @queryCompletionDateTime, @catalog, " +
                $"@database, @encryptionConfigurationKmsKey, @encryptionConfigurationEncryptionOption, @outputLocation, @statementType, " +
                $"@dataManifestLocation, @dataScannedInBytes, @engineExecutionTimeInMillis, @queryPlanningTimeInMillis, @queryQueueTimeInMillis, " +
                $"@serviceProcessingTimeInMillis, @totalExecutionTimeInMillis, @athenaError, @returnStateOfQueryRun, @returnStateChangeReason, @workGroup) RETURNING sherloq_query_execution_id;";

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
            var sherloqQueryExecutionId = int.Parse(cmd.ExecuteScalar().ToString());

            return sherloqQueryExecutionId;
        }

        internal static Database WriteNewDatabase(DbConnection dbConnection, Database database)
        {
            dbConnection.OpenConnection();
            var sql = $"INSERT INTO {ClientResources.AppsFlyerSchemaName}.{ClientResources.DatabasesTableName} (db_name, db_description) VALUES(@db_name, @db_description) RETURNING db_id";
            var cmd = new NpgsqlCommand(sql, dbConnection.connection);
            cmd.Parameters.AddWithValue("@db_name", database.name);
            cmd.Parameters.AddWithValue("@db_description", database.description);
            var dbId = int.Parse(cmd.ExecuteScalar().ToString());
            database.dbId = dbId;
            dbConnection.CloseConnection();

            return database;
        }

        internal static Schema WriteNewSchema(DbConnection dbConnection, Schema schema)
        {
            dbConnection.OpenConnection();
            var sql = $"INSERT INTO {ClientResources.AppsFlyerSchemaName}.{ClientResources.SchemasTableName} (schema_name, schema_description, db_id) VALUES(@schemaName, @schemaDescription, @dbId) RETURNING schema_id";
            var cmd = new NpgsqlCommand(sql, dbConnection.connection);
            cmd.Parameters.AddWithValue("@schemaName", schema.name);
            cmd.Parameters.AddWithValue("@schemaDescription", schema.description);
            cmd.Parameters.AddWithValue("@dbId", schema.database.dbId);
            var schemaId = int.Parse(cmd.ExecuteScalar().ToString());
            schema.schemaId = schemaId;
            dbConnection.CloseConnection();

            return schema;
        }

        internal static Table WriteNewTable(DbConnection dbConnection, Table table)
        {
            dbConnection.OpenConnection();
            var sql = $"INSERT INTO {ClientResources.AppsFlyerSchemaName}.{ClientResources.TablesTableName} (table_name, table_description, schema_id, db_id) VALUES(@tableName, @tableDescription, @schemaId, @dbId) RETURNING table_id";
            var cmd = new NpgsqlCommand(sql, dbConnection.connection);
            cmd.Parameters.AddWithValue("@tableName", table.name);
            cmd.Parameters.AddWithValue("@tableDescription", table.description);
            cmd.Parameters.AddWithValue("@schemaId", table.schema.schemaId);
            cmd.Parameters.AddWithValue("@dbId", table.schema.database.dbId);
            var tableId = int.Parse(cmd.ExecuteScalar().ToString());
            table.tableId = tableId;
            cmd.ExecuteNonQuery();
            dbConnection.CloseConnection();

            return table;
        }

        internal static Field WriteNewField(DbConnection dbConnection, Field field)
        {
            dbConnection.OpenConnection();
            var sql = $"INSERT INTO {ClientResources.AppsFlyerSchemaName}.{ClientResources.FieldsTableName} (field_name, field_description, field_type, table_id, schema_id, db_id) VALUES(@fieldName, @fieldDescription, @fieldType, @tableId, @schemaId, @dbId) RETURNING field_id";
            var cmd = new NpgsqlCommand(sql, dbConnection.connection);
            cmd.Parameters.AddWithValue("@fieldName", field.name);
            cmd.Parameters.AddWithValue("@fieldDescription", field.description);
            cmd.Parameters.AddWithValue("@fieldType", field.fieldType.ToString());
            cmd.Parameters.AddWithValue("@tableId", field.table.tableId);
            cmd.Parameters.AddWithValue("@schemaId", field.table.schema.schemaId);
            cmd.Parameters.AddWithValue("@dbId", field.table.schema.database.dbId);
            var fieldId = int.Parse(cmd.ExecuteScalar().ToString());
            field.fieldId = fieldId;
            cmd.ExecuteNonQuery();
            dbConnection.CloseConnection();

            return field;
        }

        internal static Query WriteNewQuery(DbConnection dbConnection, Query query)
        {
            dbConnection.OpenConnection();
            var sql = $"INSERT INTO {ClientResources.AppsFlyerSchemaName}.{ClientResources.QueriesTableName} (query, query_description, query_name, sherloq_query_executions) VALUES(@query, @queryDescription, @queryName, @sherloqQueryExecutions) RETURNING query_id";
            var cmd = new NpgsqlCommand(sql, dbConnection.connection);
            cmd.Parameters.AddWithValue("@query", query.query);
            cmd.Parameters.AddWithValue("@queryDescription", query.description);
            cmd.Parameters.AddWithValue("@queryName", query.name);
            cmd.Parameters.AddWithValue("@sherloqQueryExecutions", query.sherloqQueryExecutions);
            var queryId = int.Parse(cmd.ExecuteScalar().ToString());
            query.queryId = queryId;
            dbConnection.CloseConnection();

            return query;
        }

        internal static void WriteFieldToAccessLog(DbConnection dbConnection, Field field, Query query, DateTime querySubmissionDateTime, int sherloqQueryExecutionId)
        {
            dbConnection.OpenConnection();
            var sql = $"INSERT INTO {ClientResources.AppsFlyerSchemaName}.{ClientResources.ParsedQueriesFieldAccessLog} (field_id, table_id, schema_id, db_id, query_id sherloq_query_execution_id, query_submission_datetime) VALUES(@fieldId, @tableId, @schemaId, @dbId, @queryId, @sherloqQueryExecutionId, @querySubmissionDatetime)";
            var cmd = new NpgsqlCommand(sql, dbConnection.connection);
            cmd.Parameters.AddWithValue("@fieldId", field.fieldId);
            cmd.Parameters.AddWithValue("@tableId", field.table.tableId);
            cmd.Parameters.AddWithValue("@schemaId", field.table.schema.schemaId);
            cmd.Parameters.AddWithValue("@dbId", field.table.schema.database.dbId);
            cmd.Parameters.AddWithValue("@queryId", query.queryId);
            cmd.Parameters.AddWithValue("@sherloqQueryExecutionId", sherloqQueryExecutionId);
            cmd.Parameters.AddWithValue("@querySubmissionDatetime", querySubmissionDateTime);
            cmd.ExecuteNonQuery();
            dbConnection.CloseConnection();
        }

        //internal static void WriteFieldWithUnclearPath(DbConnection dbConnection, string fieldName, List<int> possibleTableIds, Query query)
        //{
        //    dbConnection.OpenConnection();
        //    var sql = $"INSERT INTO {ClientResources.AppsFlyerSchemaName}.{ClientResources.ParsedQueriesFieldAccessLog} (field_name, table_id, schema_id, db_id, query_id sherloq_query_execution_id, query_submission_datetime) VALUES(@fieldId, @tableId, @schemaId, @dbId, @queryId, @sherloqQueryExecutionId, @querySubmissionDatetime)";
        //    var cmd = new NpgsqlCommand(sql, dbConnection.connection);
        //    cmd.Parameters.AddWithValue("@fieldId", field.fieldId);
        //    cmd.Parameters.AddWithValue("@tableId", field.table.tableId);
        //    cmd.Parameters.AddWithValue("@schemaId", field.table.schema.schemaId);
        //    cmd.Parameters.AddWithValue("@dbId", field.table.schema.database.dbId);
        //    cmd.Parameters.AddWithValue("@queryId", query.queryId);
        //    cmd.Parameters.AddWithValue("@sherloqQueryExecutionId", "");
        //    cmd.Parameters.AddWithValue("@querySubmissionDatetime", querySubmissionDateTime);
        //    cmd.ExecuteNonQuery();
        //    dbConnection.CloseConnection();
        //}

        //Get db from db or null if db doesn't exist
        internal static Database GetDb(DbConnection dbConnection, string dbName)
        {
            dbConnection.OpenConnection();
            var sql = $"SELECT * FROM {ClientResources.AppsFlyerSchemaName}.{ClientResources.DatabasesTableName} WHERE db_name = @dbName";
            Database db = null;

            using (NpgsqlCommand cmd = new NpgsqlCommand(sql, dbConnection.connection))
            {
                cmd.Parameters.AddWithValue("@dbName", dbName);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                reader.Read();

                if (!reader.HasRows)
                {
                    dbConnection.CloseConnection();
                    return null;
                }

                var dbId = int.Parse(reader["db_id"].ToString());
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
            var sql = $"SELECT * FROM {ClientResources.AppsFlyerSchemaName}.{ClientResources.SchemasTableName} WHERE schema_name = @schemaName AND db_id = @dbId";
            Schema schema = null;

            dbConnection.OpenConnection();
            using (NpgsqlCommand cmd = new NpgsqlCommand(sql, dbConnection.connection))
            {
                cmd.Parameters.AddWithValue("@schemaName", schemaName);
                cmd.Parameters.AddWithValue("@dbId", db.dbId);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                reader.Read();

                if (!reader.HasRows)
                {
                    dbConnection.CloseConnection();
                    return null;
                }

                var schemaId = int.Parse(reader["schema_id"].ToString());
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
            var sql = $"SELECT * FROM {ClientResources.AppsFlyerSchemaName}.{ClientResources.TablesTableName} WHERE table_name = @tableName AND schema_id = @schemaId";
            Table table = null;

            dbConnection.OpenConnection();
            using (NpgsqlCommand cmd = new NpgsqlCommand(sql, dbConnection.connection))
            {
                cmd.Parameters.AddWithValue("@tableName", tableName);
                cmd.Parameters.AddWithValue("@schemaId", schema.schemaId);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                reader.Read();

                if (!reader.HasRows)
                {
                    dbConnection.CloseConnection();
                    return null;
                }

                var tableId = int.Parse(reader["table_id"].ToString());
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
            var sql = $"SELECT * FROM {ClientResources.AppsFlyerSchemaName}.{ClientResources.FieldsTableName} WHERE field_name = @fieldName AND table_id = @tableId";
            Field field = null;

            dbConnection.OpenConnection();
            using (NpgsqlCommand cmd = new NpgsqlCommand(sql, dbConnection.connection))
            {
                cmd.Parameters.AddWithValue("@fieldName", fieldName);
                cmd.Parameters.AddWithValue("@tableId", table.tableId);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                reader.Read();

                if (!reader.HasRows)
                {
                    dbConnection.CloseConnection();
                    return null;
                }

                var fieldId = int.Parse(reader["field_id"].ToString());
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

                var queryId = int.Parse(reader["query_id"].ToString());
                var queryDescription = reader["query_description"].ToString();
                var queryName = reader["query_name"].ToString();
                var sherloqQueryExecutions = Convert.IsDBNull(reader["sherloq_query_executions"]) ? null : (int[])reader["sherloq_query_executions"];
                query = new Query(queryString, queryName, queryDescription, queryId, sherloqQueryExecutions);
            }
            dbConnection.CloseConnection();

            return query;
        }

        internal static void AddQueryExecutionIdToListInQuery(DbConnection dbConnection, Query query, int sherloqQueryExecutionId)
        {
            var sql = $"UPDATE {ClientResources.AppsFlyerSchemaName}.{ClientResources.QueriesTableName} SET sherloq_query_executions = array_append(sherloq_query_executions, @sherloqQueryExecutionId) WHERE query = @query";
            
            dbConnection.OpenConnection();
            using (NpgsqlCommand cmd = new NpgsqlCommand(sql, dbConnection.connection))
            {
                cmd.Parameters.AddWithValue("@query", query.query);
                cmd.Parameters.AddWithValue("@sherloqQueryExecutionId", sherloqQueryExecutionId);
                cmd.ExecuteNonQuery();
            }
            dbConnection.CloseConnection();
        }
    }
}