namespace SqlQueryParsing.DataAssets
{
    internal class Query : DataAsset
    {
        string query;
        Field[] fields;

        internal Query(string name, string description, AssetTypeEnum assetType, string query, Field[] fields) : base(name, description, assetType)
        {
            this.query = query;
            this.fields = fields;
        }
    }
}
