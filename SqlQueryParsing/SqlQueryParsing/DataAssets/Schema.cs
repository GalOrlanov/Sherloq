namespace SqlQueryParsing.DataAssets
{
    internal class Schema : DataAsset
    {
        internal Table[] tables;
        internal Database database;
        internal int schema_id;

        internal Schema(string name, Database database, int schema_id = 0, string description = "", Table[] tables = null) : base(name, description, AssetTypeEnum.Schema)
        {
            this.tables = tables;
            this.schema_id = schema_id;
            this.database = database;
        }
    }
}