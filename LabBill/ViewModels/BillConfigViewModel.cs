using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using LabBill.Core.Contracts.Services;
using LabBill.Core.Domains;
using SQLitePCL;

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

    private readonly IBillDataService _billDataService;

    public BillConfigViewModel(IBillDataService billDataService)
    {
        _billDataService = billDataService;

        People.Clear();
        var people = _billDataService.getAllPeople();
        foreach (var person in people)
        {
            People.Add(person);
        }
    }

    public void AddNewPerson(string personName)
    {
        Person newPerson = new Person
        {
            Name = personName
        };

        _billDataService.addPerson(newPerson);

        People.Clear();
        var people = _billDataService.getAllPeople();
        foreach (var person in people)
        {
            People.Add(person);
        }
    }
}
