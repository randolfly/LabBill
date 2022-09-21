using LabBill.Core.Services;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LabBill.Core.Tests.MSTest;

[TestClass]
public class SampleDataServiceTests
{
    public SampleDataServiceTests()
    {
    }

    // Remove or update this once your app is using real data and not the SampleDataService.
    // This test serves only as a demonstration of testing functionality in the Core library.
    [TestMethod]
    public async Task EnsureSampleDataServiceReturnsListDetailsDataAsync()
    {
        var sampleDataService = new SampleDataService();

        var data = await sampleDataService.GetListDetailsDataAsync();

        Assert.IsTrue(data.Any());
    }
}
