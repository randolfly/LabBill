using LabBill.Core.Domains;
using LabBill.Core.Models;

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace LabBill.Views;

public sealed partial class BillDetailDetailControl : UserControl
{
    //public SampleOrder? ListDetailsMenuItem
    //{
    //    get => GetValue(ListDetailsMenuItemProperty) as SampleOrder;
    //    set => SetValue(ListDetailsMenuItemProperty, value);
    //}

    public Bill? ListDetailsMenuItem
    {
        get => GetValue(ListDetailsMenuItemProperty) as Bill;
        set => SetValue(ListDetailsMenuItemProperty, value);
    }

    public static readonly DependencyProperty ListDetailsMenuItemProperty = DependencyProperty.Register("ListDetailsMenuItem", typeof(Bill), typeof(BillDetailDetailControl), new PropertyMetadata(null, OnListDetailsMenuItemPropertyChanged));

    public BillDetailDetailControl()
    {
        InitializeComponent();
    }

    private static void OnListDetailsMenuItemPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is BillDetailDetailControl control)
        {
            control.ForegroundElement.ChangeView(0, 0, 1);
        }
    }
}
