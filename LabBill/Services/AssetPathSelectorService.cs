using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabBill.Contracts.Services;
using Microsoft.UI.Xaml.Controls.Primitives;

namespace LabBill.Services;
public class AssetPathSelectorService : IAssetPathSelectorService
{
    private const string SettingsKey = "AppAssetsDirPath";

    public string AssetPath
    {
        get;
        set;
    } = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

    private readonly ILocalSettingsService _localSettingsService;

    public AssetPathSelectorService(ILocalSettingsService localSettingsService)
    {
        _localSettingsService = localSettingsService;
    }

    public async Task InitializeAsync()
    {
        AssetPath = await _localSettingsService.ReadSettingAsync<string>(SettingsKey) ?? Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        await Task.CompletedTask;
    }
    public async Task SetAssetPathAsync(string dirPath)
    {
        await _localSettingsService.SaveSettingAsync(SettingsKey, dirPath);
        await Task.CompletedTask;
    }
}
