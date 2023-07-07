using ExcelApp.Data;
using ExcelApp.Helpers;


namespace ExcelApp.Test;

[TestFixture]
public class DataMergerTests
{
    private List<ProductInfo> _expectedMergeItems = new();
    private DataMerger _dataMerger;


    [SetUp]
    public void SetUp()
    {
        _dataMerger = new DataMerger();
        _expectedMergeItems = TestDataUtility.CreateMergeExpectedItems();
    }


    [Test]
    public void MergeData_ValidItems_ReturnsCorrectMergedData()
    {
        var testItems = CreateTestItems();
        var result = _dataMerger.MergeData(testItems[0], testItems[1]);
        Assert.That(_expectedMergeItems.OrderBy(x => x.Id), Is.EquivalentTo(result.OrderBy(x => x.Id))
            .Using(new MyItemEqualityComparer()));
    }

    [Test]
    public void MergeData_EffectiveDateFrom_ReturnsCorrectMergedData()
    {
        var testItems = CreateTestItems();
        testItems[0][0].EffectiveDateFrom = new DateTime(2022, 02, 01);
        var result = _dataMerger.MergeData(testItems[0], testItems[1]);
        Assert.That(_expectedMergeItems.OrderBy(x => x.Id), Is.EquivalentTo(result.OrderBy(x => x.Id))
            .Using(new MyItemEqualityComparer()));
    }

    [Test]
    public void MergeData_EffectiveDateFrom_ReturnsIncorrectMergedData()
    {
        var testItems = CreateTestItems();
        testItems[0][0].EffectiveDateFrom = new DateTime(2021, 01, 01);
        var result = _dataMerger.MergeData(testItems[0], testItems[1]);
        Assert.That(_expectedMergeItems.OrderBy(x => x.Id), !Is.EquivalentTo(result.OrderBy(x => x.Id))
            .Using(new MyItemEqualityComparer()));
    }

    [Test]
    public void MergeData_EffectiveDateBy_ReturnsCorrectMergedData()
    {
        var testItems = CreateTestItems();
        testItems[0][0].EffectiveDateBy = new DateTime(2020, 02, 01);
        var result = _dataMerger.MergeData(testItems[0], testItems[1]);
        Assert.That(_expectedMergeItems.OrderBy(x => x.Id), Is.EquivalentTo(result.OrderBy(x => x.Id))
            .Using(new MyItemEqualityComparer()));
    }

    [Test]
    public void MergeData_EffectiveDateBy_ReturnsIncorrectMergedData()
    {
        var testItems = CreateTestItems();
        testItems[0][0].EffectiveDateBy = new DateTime(2023, 01, 01);
        var result = _dataMerger.MergeData(testItems[0], testItems[1]);
        Assert.That(_expectedMergeItems.OrderBy(x => x.Id), !Is.EquivalentTo(result.OrderBy(x => x.Id))
            .Using(new MyItemEqualityComparer()));
    }

    private List<List<ProductInfo>> CreateTestItems()
    {
        var mainData = new List<ProductInfo>()
        {
            new()
            {
                Id = 1,
                Name = "Item 1",
                Cipher = "Cipher 1",
                EffectiveDateFrom = new DateTime(2022, 01, 01),
                EffectiveDateBy = new DateTime(2022, 01, 05),
            },
            new()
            {
                Id = 2,
                Name = "Item 2",
                Cipher = "Cipher 2",
                EffectiveDateFrom = new DateTime(2022, 01, 03),
                EffectiveDateBy = new DateTime(2022, 01, 10)
            }
        };

        var dataForMerge = new List<ProductInfo>()
        {
            new()
            {
                Id = 3,
                Name = "TestName",
                Cipher = "Cipher 1",
                EffectiveDateFrom = new DateTime(2022, 01, 01),
                EffectiveDateBy = new DateTime(2022, 01, 05),
            },
            new()
            {
                Id = 4,
                Name = "Item 3",
                Cipher = "Cipher 3",
                EffectiveDateFrom = new DateTime(2022, 01, 03),
                EffectiveDateBy = new DateTime(2022, 01, 10)
            }
        };
        return new List<List<ProductInfo>>()
        {
            mainData,
            dataForMerge
        };
    }
}