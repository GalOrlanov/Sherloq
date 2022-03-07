namespace SqlQueryParsing.DataAssets
{
    internal class Database : DataAsset
    {
        internal Schema[] schemas;
        internal int dbId;

        internal Database(string name, int dbId = 0, string description = "", Schema[] schemas = null) : base(name, description, AssetTypeEnum.Database)
        {
            this.schemas = schemas;
            this.dbId = dbId;
        }
    }
}