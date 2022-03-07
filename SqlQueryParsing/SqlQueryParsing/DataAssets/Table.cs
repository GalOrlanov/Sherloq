namespace SqlQueryParsing.DataAssets
{
    internal class Table : DataAsset
    {
        internal Field[] fields;
        internal Schema schema;
        internal int table_id;

        internal Table(string name, Schema schema = null, int table_id = 0, string description = "", Field[] fields = null) : base(name, description, AssetTypeEnum.Table)
        {
            this.fields = fields;
            this.schema = schema;
            this.table_id = table_id;
        }

        public override string ToString()
        {
            return $"Table name: {name}";
        }
    }
}