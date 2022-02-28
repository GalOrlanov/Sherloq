namespace SqlQueryParsing.DataAssets
{
    internal class Scheme : DataAsset
    {
        Table[] tables;

        internal Scheme(string name, string description, AssetTypeEnum assetType, Table[] tables) : base(name, description, assetType)
        {
            this.tables = tables;
        }
    }
}