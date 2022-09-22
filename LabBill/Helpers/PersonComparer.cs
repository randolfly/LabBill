using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabBill.Core.Domains;

namespace LabBill.Helpers;
class PersonComparer : IEqualityComparer<Person>
{
    public bool Equals(Person x, Person y)
    {
        return x.Name == y.Name;
    }

    public int GetHashCode(Person p)
    {
        return p.GetHashCode();
    }
}