namespace SqlQueryParsing.DataAssets
{
    internal class Table : DataAsset
    {
        Field[] fields;
        Scheme scheme;

        internal Table(string name, Scheme scheme = null, string description = null, Field[] fields = null) : base(name, description, AssetTypeEnum.Table)
        {
            this.fields = fields;
            this.scheme = scheme;
        }

        public override string ToString()
        {
            return $"Table name: {name}";
        }
    }
}