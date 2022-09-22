using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabBill.Core.Domains;

namespace LabBill.Helpers;
class AssetComparer : IEqualityComparer<AssetInfo>
{
    public bool Equals(AssetInfo x, AssetInfo y)
    {
        return x.AssetLink == y.AssetLink;
    }

    public int GetHashCode(AssetInfo asset)
    {
        return asset.GetHashCode();
    }
}