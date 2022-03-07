namespace SqlQueryParsing.DataAssets
{
    internal class Field : DataAsset
    {
        internal string fieldType;
        internal Table table;
        internal int field_id;

        internal Field(string name, Table table, string type, int field_id = 0, string description = "") : base(name, description, AssetTypeEnum.Field)
        {
            this.fieldType = type;
            this.table = table;
            this.field_id = field_id;
        }

        public override string ToString()
        {
            var result = $"Field name: {name}" + (table == null ? "" : $"\n\tTable name: {table.name}"); 
            return result;
        }
    }
}