using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabBill.Core.Domains;

namespace LabBill.Core.Contracts.Services;
public interface IBillDataService
{
    #region Person Interface
    public void AddPerson(Person person);
    public bool DelPerson(Person person);
    public IEnumerable<Person> GetAllPeople();
    public Person UpdatePerson(Person person);
    #endregion

    #region Bill Interface
    public Bill UpdateBill(Bill bill);
    public bool DeleteBill(int id);
    public IEnumerable<Bill> GetAllBills();
    public IEnumerable<Bill> GetAllUnFinishedBills();
    public IEnumerable<Bill> GetAllFinishedBills();
    #endregion
}
