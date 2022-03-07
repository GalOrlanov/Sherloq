using System.Collections.Generic;

namespace SqlQueryParsing.DataAssets
{
    internal class Query : DataAsset
    {
        internal string query;
        internal List<Field> fields;
        internal int query_id;
        internal int[] sherloq_query_executions;

        internal Query(string query, string name = "", string description = "", int query_id = 0, int[] sherloq_query_executions = null, List<Field> fields = null) : base(name, description, AssetTypeEnum.Query)
        {
            this.query = query;
            this.fields = fields;
            this.query_id = query_id;
            this.sherloq_query_executions = sherloq_query_executions;
        }
    }
}
