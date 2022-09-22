using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml;

namespace LabBill.Contracts.Services;
public interface IAssetPathSelectorService
{
    string AssetPath
    {
        get;
    }

    Task InitializeAsync();

    Task<string> GetAssetPathAsync();
    Task SetAssetPathAsync(string dirPath);
}
