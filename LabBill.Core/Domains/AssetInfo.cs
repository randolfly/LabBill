using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabBill.Core.Domains;

public class AssetInfo
{
    [Key]
    public int Id
    {
        get; set;
    }
    public string AssetLink
    {
        get; set;
    }
    public string Comment
    {
        get; set;
    }
}
