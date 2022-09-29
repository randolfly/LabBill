using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;

using LabBill.Contracts.ViewModels;
using LabBill.Core.Contracts.Services;
using LabBill.Core.Domains;
using LabBill.Core.Models;

namespace LabBill.ViewModels;

public class BillDetailViewModel : ObservableRecipient, INavigationAware
{
    private readonly ISampleDataService _sampleDataService;
    private readonly IBillDataService _billDataService;
    private SampleOrder? _selected;

    public SampleOrder? Selected
    {
        get => _selected;
        set => SetProperty(ref _selected, value);
    }

    private Bill? _selectedBill;

    public Bill? SelectedBill
    {
        get => _selectedBill;
        set => SetProperty(ref _selectedBill, value);
    }


    public ObservableCollection<SampleOrder> SampleItems { get; private set; } = new ObservableCollection<SampleOrder>();

    public ObservableCollection<Bill> Bills { get; private set; } = new ObservableCollection<Bill>();

    public BillDetailViewModel(ISampleDataService sampleDataService, IBillDataService billDataService)
    {
        _sampleDataService = sampleDataService;
        _billDataService = billDataService;
    }

    public async void OnNavigatedTo(object parameter)
    {
        SampleItems.Clear();

        // TODO: Replace with real data.
        var data = await _sampleDataService.GetListDetailsDataAsync();

        foreach (var item in data)
        {
            SampleItems.Add(item);
        }

        Bills.Clear();
        var billData = _billDataService.GetAllBills();

        foreach (var item in billData)
        {
            Bills.Add(item);
        }
    }

    public void OnNavigatedFrom()
    {
    }

    public void EnsureItemSelected()
    {
        if (Selected == null)
        {
            Selected = SampleItems.First();
        }
    }
}
