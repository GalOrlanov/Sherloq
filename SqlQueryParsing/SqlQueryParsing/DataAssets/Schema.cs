namespace SqlQueryParsing.DataAssets
{
    internal class Schema : DataAsset
    {
        internal Table[] tables;
        internal Database database;
        internal int schemaId;

        internal Schema(string name, Database database, int schemaId = 0, string description = "", Table[] tables = null) : base(name, description, AssetTypeEnum.Schema)
        {
            this.tables = tables;
            this.schemaId = schemaId;
            this.database = database;
        }
    }
}