namespace SqlQueryParsing.DataAssets
{
    internal class Database : DataAsset
    {
        internal Schema[] schemas;
        internal int db_id;

        internal Database(string name, int db_id = 0, string description = "", Schema[] schemas = null) : base(name, description, AssetTypeEnum.Database)
        {
            this.schemas = schemas;
            this.db_id = db_id;
        }
    }
}