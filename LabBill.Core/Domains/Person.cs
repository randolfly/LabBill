using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace LabBill.Core.Domains;
public class Person
{
    [Key]
    public int Id
    {
        get; set;
    }
    public string Name
    {
        get; set;
    }

    public IEnumerable<Bill> Bills
    {
        get; set;
    }

    public override string ToString()
    {
        return Name;
    }
}
