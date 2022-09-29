using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using LabBill.Contracts.Services;
using LabBill.Core.Contracts.Services;
using LabBill.Core.Domains;
using LabBill.Helpers;
using Microsoft.UI.Xaml.Controls;
using SQLitePCL;
using Windows.Storage;

namespace LabBill.ViewModels;

public class BillConfigViewModel : ObservableRecipient
{
    private Bill _bill = new Bill
    {
        // 绑定在界面的日历上，尽管只显示到日期，但是实际上时间是按照操作时的时间进行记录的
        // 这么做主要是考虑到界面简洁性
        DateTime = DateTime.Now
    };

    public Bill Bill
    {
        get => _bill;
        set => SetProperty(ref _bill, value);
    }


    public ObservableCollection<Person> People = new();


    public ObservableCollection<AssetInfo> AssetInfos { get; set; } = new ObservableCollection<AssetInfo>();

    private AssetInfo _selectedAsset;
    public AssetInfo SelectedAsset
    {
        get
        {
            return _selectedAsset;
        }
        set
        {
            SetProperty(ref _selectedAsset, value);
        }
    }

    public string FolderPath = new("");

    private readonly AssetComparer AssetComparer = new();

    private readonly IBillDataService _billDataService;
    private readonly IAssetPathSelectorService _assetPathSelectorService;

    public BillConfigViewModel(IBillDataService billDataService, IAssetPathSelectorService assetPathSelectorService)
    {
        _billDataService = billDataService;
        _assetPathSelectorService = assetPathSelectorService;

        People.Clear();
        var people = _billDataService.GetAllPeople();
        foreach (var person in people)
        {
            People.Add(person);
        }

        FolderPath = _assetPathSelectorService.AssetPath;
    }

    public void AddNewPerson(string personName)
    {
        Person newPerson = new Person
        {
            Name = personName
        };

        _billDataService.AddPerson(newPerson);

        People.Clear();
        var people = _billDataService.GetAllPeople();
        foreach (var person in people)
        {
            People.Add(person);
        }
    }

    public void AddAssets(IReadOnlyList<IStorageItem> items)
    {
        foreach (var item in items)
        {
            var storageFile = item as StorageFile;
            var tempAsset = new AssetInfo
            {
                AssetLink = storageFile.Path
            };
            if (!AssetInfos.Contains(tempAsset, AssetComparer))
            {
                AssetInfos.Add(tempAsset);
            }
        }
        Bill.AssetInfos = AssetInfos;
    }

    public void AddNewBill()
    {
        // pre-config for bill
        if (Bill.AssetInfos != null)
        {
            string folderRootPath = FolderPath;
            string billYear = Bill.DateTime.ToString("yyyy");
            string billMonth = Bill.DateTime.ToString("MM");

            string dirPath = Path.Combine(folderRootPath, billYear, billMonth);
            Directory.CreateDirectory(dirPath);

            foreach (var asset in Bill.AssetInfos)
            {
                string assetName = Path.GetFileName(asset.AssetLink);
                string targetFileName = $"{Bill.DateTime.ToString("D")}_{Bill.Person.Name}_{assetName}";
                string relPath = Path.Combine(billYear, billMonth, targetFileName);

                string targetPath = Path.Combine(folderRootPath, relPath);
                File.Copy(asset.AssetLink, targetPath, true);

                asset.AssetLink = relPath;
            }
        }
        _billDataService.UpdateBill(Bill);
        Bill = new Bill { DateTime = DateTime.Now };
        AssetInfos.Clear();
    }
}
