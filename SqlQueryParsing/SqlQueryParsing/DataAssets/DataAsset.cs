using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlQueryParsing.DataAssets
{
    public class DataAsset
    {
        internal string name;
        internal string dscription;
        internal AssetTypeEnum assetType;

        internal DataAsset(string name, string description, AssetTypeEnum assetType)
        {
            this.name = name;
            this.dscription = description;
            this.assetType = assetType;
        }
    }
}
