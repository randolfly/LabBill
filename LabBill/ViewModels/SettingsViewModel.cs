using System.Reflection;
using System.Windows.Input;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using LabBill.Contracts.Services;
using LabBill.Helpers;

using Microsoft.UI.Xaml;
using Windows.ApplicationModel;

namespace LabBill.ViewModels;

public class SettingsViewModel : ObservableRecipient
{
    private readonly IThemeSelectorService _themeSelectorService;
    private readonly IAssetPathSelectorService _assetPathSelectorService;

    private ElementTheme _elementTheme;
    private string _versionDescription;
    private string _folderPath;
    public string FolderPath
    {
        get
        {
            return _folderPath;
        }
        set
        {
            SetProperty(ref _folderPath, value);
        }
    }

    public ElementTheme ElementTheme
    {
        get => _elementTheme;
        set => SetProperty(ref _elementTheme, value);
    }

    public string VersionDescription
    {
        get => _versionDescription;
        set => SetProperty(ref _versionDescription, value);
    }

    public ICommand SwitchThemeCommand
    {
        get;
    }

    public SettingsViewModel(IThemeSelectorService themeSelectorService, IAssetPathSelectorService assetPathSelectorService)
    {
        _assetPathSelectorService = assetPathSelectorService;
        _themeSelectorService = themeSelectorService;
        _elementTheme = _themeSelectorService.Theme;
        _folderPath = _assetPathSelectorService.AssetPath;
        _versionDescription = GetVersionDescription();

        SwitchThemeCommand = new RelayCommand<ElementTheme>(
            async (param) =>
            {
                if (ElementTheme != param)
                {
                    ElementTheme = param;
                    await _themeSelectorService.SetThemeAsync(param);
                }
            });
    }

    private static string GetVersionDescription()
    {
        Version version;

        if (RuntimeHelper.IsMSIX)
        {
            var packageVersion = Package.Current.Id.Version;

            version = new(packageVersion.Major, packageVersion.Minor, packageVersion.Build, packageVersion.Revision);
        }
        else
        {
            version = Assembly.GetExecutingAssembly().GetName().Version!;
        }

        return $"{"AppDisplayName".GetLocalized()} - {version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
    }

    public async void selectAssetDirPath(string assetDirPath)
    {
        FolderPath = assetDirPath;
        await _assetPathSelectorService.SetAssetPathAsync(assetDirPath);
    }
}
