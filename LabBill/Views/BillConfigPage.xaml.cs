using LabBill.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace LabBill.Views;

public sealed partial class BillConfigPage : Page
{
    public BillConfigViewModel ViewModel
    {
        get;
    }

    public BillConfigPage()
    {
        ViewModel = App.GetService<BillConfigViewModel>();
        InitializeComponent();
    }
}
