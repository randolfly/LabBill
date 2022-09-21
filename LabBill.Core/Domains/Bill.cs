using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using LabBill.Core.Models;

namespace LabBill.Core.Domains;

public class Bill
{
    [Key]
    public int Id
    {
        get; set;
    }

    public DateTime DateTime
    {
        get; set;
    }

    public string Brief
    {
        get; set;
    }
    /// <summary>
    /// The main character who charges this bill
    /// </summary>
    public Person Person
    {
        get; set;
    }

    /// <summary>
    /// The type of orders, e.g. 交通/travel, 设备/equipment
    /// </summary>
    public OrderType OrderType
    {
        get; set;
    }

    public decimal Price
    {
        get; set;
    }
    /// <summary>
    /// Content description of this bill
    /// </summary>
    public string Content
    {
        get; set;
    }

    public BillType BillType
    {
        get; set;
    }

    public BillState BillState
    {
        get; set;
    }

    /// <summary>
    /// The path link to the assets files, like pdf file or bill snapshot image
    /// The reason why not use binary stream to storage in the database is that
    ///     it may be not feasible for file exchangement
    /// </summary>
    public IEnumerable<AssetInfo> AssetInfos
    {
        get; set;
    }

}