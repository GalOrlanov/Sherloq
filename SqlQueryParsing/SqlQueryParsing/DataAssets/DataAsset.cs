namespace SqlQueryParsing.DataAssets
{
    public class DataAsset
    {
        internal string name;
        internal string description;
        internal AssetTypeEnum assetType;

        internal DataAsset(string name, string description, AssetTypeEnum assetType)
        {
            this.name = name;
            this.description = description;
            this.assetType = assetType;
        }
    }
}
