using LabBill.Core.Contracts.Services;
using LabBill.Core.Domains;
using LabBill.Core.Services;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabBill.Core.Tests.MSTest;

[TestClass]
public class BillDataServiceTests
{
    public IBillDataService _cashDataService
    {
        get; set;
    }
    public BillDataServiceTests()
    {
        _cashDataService = new BillDataService();
    }

    [TestInitialize]
    public void Setup()
    {
        _cashDataService = new BillDataService();
    }

    [TestMethod]
    public void AddSampleData()
    {
        var person = new Person { Name = "yxy" };
        var asset1 = new AssetInfo { AssetLink = "asass1" };
        var asset2 = new AssetInfo { AssetLink = "asass2" };
        var asset3 = new AssetInfo { AssetLink = "asass3" };
        var bill = new Bill
        {
            BillState = Models.BillState.Finished,
            Person = person,
            AssetInfos = new List<AssetInfo> { asset1, asset2, asset3 },
            DateTime = DateTime.Now
        };

        _cashDataService.UpdateBill(bill);

        var billCheck = _cashDataService.GetAllBills().Last();
        Assert.IsNotNull(billCheck);
        Assert.IsNotNull(billCheck.AssetInfos);

        Assert.IsTrue(billCheck.AssetInfos.ElementAt(0).AssetLink == asset1.AssetLink);
        Assert.IsTrue(billCheck.AssetInfos.ElementAt(1).AssetLink == asset2.AssetLink);
        Assert.IsTrue(billCheck.AssetInfos.ElementAt(2).AssetLink == asset3.AssetLink);

        Assert.IsNotNull(billCheck.Person);
        Assert.IsTrue(billCheck.Person.Name == person.Name);
    }
}
