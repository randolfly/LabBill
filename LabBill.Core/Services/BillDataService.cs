using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabBill.Core.Context;
using LabBill.Core.Contracts.Services;
using LabBill.Core.Domains;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace LabBill.Core.Services;
public class BillDataService : IBillDataService
{
    public void AddPerson(Person person)
    {
        using var db = new BillContext();
        var containsPerson = db.People.Any(x => x.Name == person.Name);

        if (!containsPerson)
        {
            db.People.Add(person);
            db.SaveChanges();
        }
    }
    public bool DeleteBill(int id)
    {
        using var db = new BillContext();
        try
        {
            var bill = db.Bills.Where(b => b.Id == id).First();
            db.Remove(bill);
            db.SaveChanges();
            return true;
        }
        catch (Exception e)
        {
            Log.Error(e.Message);
            return false;
        }
    }
    public bool DelPerson(Person person) => throw new NotImplementedException();
    public IEnumerable<Bill> GetAllBills()
    {
        using var db = new BillContext();
        var bills = db.Bills
            .Include(b => b.AssetInfos)
            .Include(b => b.Person)
            .ToList();
        return bills;
    }
    public IEnumerable<Bill> GetAllFinishedBills()
    {
        using var db = new BillContext();
        var bills = db.Bills
            .Include(b => b.AssetInfos)
            .Include(b => b.Person)
            .Where(b => b.BillState == Models.BillState.Finished)
            .ToList();
        return bills;
    }

    public IEnumerable<Person> GetAllPeople()
    {
        using var db = new BillContext();
        try
        {
            var people = db.People
            .Include(p => p.Bills)
            .ToList();
            return people;
        }
        catch (Exception e)
        {
            Log.Error(e.Message);
            return null;
        }
    }

    public IEnumerable<Bill> GetAllUnFinishedBills()
    {
        using var db = new BillContext();
        var bills = db.Bills
            .Include(b => b.AssetInfos)
            .Include(b => b.Person)
            .Where(b => b.BillState == Models.BillState.NotFinished)
            .ToList();
        return bills;
    }

    public Bill UpdateBill(Bill bill)
    {
        using var db = new BillContext();
        db.Bills.Update(bill);
        db.SaveChanges();
        return bill;
    }
    public Person UpdatePerson(Person person)
    {
        using var db = new BillContext();
        db.People.Update(person);
        db.SaveChanges();
        return person;
    }
}
