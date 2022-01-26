using System.Data;

namespace SqlQueryParsing.DataAssets
{
    internal class Field : DataAsset
    {
        SqlDbType type;
        Table table;

        internal Field(string name, Table table, string description = null, SqlDbType type = SqlDbType.Text) : base(name, description, AssetTypeEnum.Field)
        {
            this.type = type;
            this.table = table;
        }

        public override string ToString()
        {
            var result = $"Field name: {name}" + (table == null ? "" : $"\n\tTable name: {table.name}"); 
            return result;
        }
    }
}