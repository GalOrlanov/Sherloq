namespace SqlQueryParsing.DataAssets
{
    internal class Table : DataAsset
    {
        internal Field[] fields;
        internal Schema schema;
        internal int tableId;

        internal Table(string name, Schema schema = null, int tableId = 0, string description = "", Field[] fields = null) : base(name, description, AssetTypeEnum.Table)
        {
            this.fields = fields;
            this.schema = schema;
            this.tableId = tableId;
        }

        public override string ToString()
        {
            return $"Table name: {name}";
        }
    }
}