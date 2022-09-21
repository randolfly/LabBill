using CommunityToolkit.WinUI.UI.Controls;

using LabBill.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace LabBill.Views;

public sealed partial class BillDetailPage : Page
{
    public BillDetailViewModel ViewModel
    {
        get;
    }

    public BillDetailPage()
    {
        ViewModel = App.GetService<BillDetailViewModel>();
        InitializeComponent();
    }

    private void OnViewStateChanged(object sender, ListDetailsViewState e)
    {
        if (e == ListDetailsViewState.Both)
        {
            ViewModel.EnsureItemSelected();
        }
    }
}
