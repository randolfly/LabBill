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
    public void addPerson(Person person);
    public bool delPerson(Person person);
    public IEnumerable<Person> getAllPeople();
    public Person updatePerson(Person person);
    #endregion

    #region Bill Interface
    public Bill updateBill(Bill bill);
    public bool deleteBill(int id);
    public IEnumerable<Bill> getAllBills();
    public IEnumerable<Bill> getAllUnFinishedBills();
    public IEnumerable<Bill> getAllFinishedBills();
    #endregion
}
