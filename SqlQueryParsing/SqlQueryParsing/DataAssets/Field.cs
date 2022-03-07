namespace SqlQueryParsing.DataAssets
{
    internal class Field : DataAsset
    {
        internal string fieldType;
        internal Table table;
        internal int fieldId;

        internal Field(string name, Table table, string type, int fieldId = 0, string description = "") : base(name, description, AssetTypeEnum.Field)
        {
            this.fieldType = type;
            this.table = table;
            this.fieldId = fieldId;
        }

        public override string ToString()
        {
            var result = $"Field name: {name}" + (table == null ? "" : $"\n\tTable name: {table.name}"); 
            return result;
        }
    }
}