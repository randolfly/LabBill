using System.Collections.ObjectModel;
using LabBill.Core.Domains;
using LabBill.Core.Models;
using LabBill.ViewModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.Web.WebView2.Core;
using Serilog;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage;

namespace LabBill.Views;

public sealed partial class BillConfigPage : Page
{
    public BillConfigViewModel ViewModel
    {
        get;
    }


    readonly List<string> OrderTypes = new();
    readonly List<string> BillTypes = new List<string>
    {
        "Reimbursable",
        "NonReimbursable"
    };
    readonly List<string> BillStates = new List<string>
    {
        "Finished",
        "NotFinished"
    };

    public BillConfigPage()
    {
        ViewModel = App.GetService<BillConfigViewModel>();
        InitializeComponent();

        var orderTypes = Enum.GetValues((typeof(OrderType)));
        foreach (OrderType enumValue in (OrderType[])orderTypes)
        {
            OrderTypes.Add(enumValue.ToString());
        }
    }

    private void OrderType_RadioButton_Checked(object sender, RoutedEventArgs e)
    {
        if (sender is RadioButtons rb)
        {
            // would invoke twice, the first time the SelectedItem is not updated
            if (rb.SelectedItem is string orderType)
            {
                ViewModel.Bill.OrderType = (OrderType)Enum.Parse(typeof(OrderType), orderType);
            }
        }
    }

    private void BillType_RadioButton_Checked(object sender, RoutedEventArgs e)
    {
        if (sender is RadioButtons rb)
        {
            // would invoke twice, the first time the SelectedItem is not updated
            if (rb.SelectedItem is string billType)
            {
                ViewModel.Bill.BillType = (BillType)Enum.Parse(typeof(BillType), billType);
            }
        }
    }

    private void BillState_RadioButton_Checked(object sender, RoutedEventArgs e)
    {
        if (sender is RadioButtons rb)
        {
            // would invoke twice, the first time the SelectedItem is not updated
            if (rb.SelectedItem is string billState)
            {
                ViewModel.Bill.BillState = (BillState)Enum.Parse(typeof(BillState), billState);
            }
        }
    }

    private void AddNewPerson(object sender, RoutedEventArgs e)
    {
        var personName = newPersonName_flyout.Text ?? "randolf";
        ViewModel.AddNewPerson(personName);
        ViewModel.Bill.Person = ViewModel.People.Last();
        people_radioBtn.SelectedItem = ViewModel.People.Last();
    }

    private async void AddNewBill(object sender, RoutedEventArgs e)
    {
        if (ViewModel.Bill.Person == null)
        {
            ContentDialog noPersonDialog = new ContentDialog();

            // XamlRoot must be set in the case of a ContentDialog running in a Desktop app
            noPersonDialog.XamlRoot = this.XamlRoot;
            noPersonDialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            noPersonDialog.Title = "No Person Selection";
            noPersonDialog.CloseButtonText = "Ok";
            noPersonDialog.DefaultButton = ContentDialogButton.Primary;
            noPersonDialog.Content = "Please choose 1 person";

            var result = await noPersonDialog.ShowAsync();

        }
        else
        {
            ViewModel.AddNewBill();
        }
    }

    private void DragOver_ListView(object sender, DragEventArgs e)
    {
        // Trash only accepts text
        e.AcceptedOperation = DataPackageOperation.Link;
    }

    private async void Drop_ListView(object sender, DragEventArgs e)
    {
        // FIXME: 提交订单后无法插入新Asset
        if (e.DataView.Contains(StandardDataFormats.StorageItems))
        {
            DragOperationDeferral def = e.GetDeferral();
            var items = await e.DataView.GetStorageItemsAsync();
            if (items.Count > 0)
            {
                ViewModel.AddAssets(items);
            }
            e.AcceptedOperation = DataPackageOperation.Link;
            def.Complete();
        }       
    }

    private void AssetInfos_ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        try
        {
            string pdfPath = ViewModel.SelectedAsset.AssetLink;
            // MessageBox.Show("预览PDF");
            WebView2.CoreWebView2.Navigate(pdfPath);
        }
        catch (System.NullReferenceException)
        {
            // delete the ViewModel.SelectedAsset
            WebView2.CoreWebView2.Navigate("https://randolfly.github.io/");
        }
    }

    private void AssetInfos_ListView_KeyDown(object sender, Microsoft.UI.Xaml.Input.KeyRoutedEventArgs e)
    {
        if (e.Key == Windows.System.VirtualKey.Delete)
        {
            Log.Information("Delete Selected Assets!");
            ViewModel.AssetInfos.Remove(ViewModel.SelectedAsset);
            ViewModel.SelectedAsset = new AssetInfo();
        }
    }
}
