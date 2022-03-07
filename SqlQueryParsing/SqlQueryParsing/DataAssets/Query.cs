using System.Collections.Generic;

namespace SqlQueryParsing.DataAssets
{
    internal class Query : DataAsset
    {
        internal string query;
        internal List<Field> fields;
        internal int queryId;
        internal int[] sherloqQueryExecutions;

        internal Query(string query, string name = "", string description = "", int queryId = 0, int[] sherloqQueryExecutions = null, List<Field> fields = null) : base(name, description, AssetTypeEnum.Query)
        {
            this.query = query;
            this.fields = fields;
            this.queryId = queryId;
            this.sherloqQueryExecutions = sherloqQueryExecutions;
        }
    }
}
