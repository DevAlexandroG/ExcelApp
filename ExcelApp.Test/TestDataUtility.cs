using ExcelApp.Data;

namespace ExcelApp.Test;

public static class TestDataUtility
{
    public static List<ProductInfo> CreateExpectedItems()
    {
        return new List<ProductInfo>
        {
            new()
            {
                Id = 1,
                Name = "Item 1",
                Cipher = "Cipher 1",
                EffectiveDateFrom = new DateTime(2022, 01, 01),
                EffectiveDateBy = new DateTime(2022, 01, 05)
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
    }

    public static List<ProductInfo> CreateMergeExpectedItems()
    {
        return new List<ProductInfo>
        {
            new()
            {
                Id = 1,
                Name = "Item 1",
                Cipher = "Cipher 1",
                EffectiveDateFrom = new DateTime(2022, 01, 01),
                EffectiveDateBy = new DateTime(2022, 01, 05),
                ExitID = 3,
                IsExt = 0,
            },
            new()
            {
                Id = 2,
                Name = "Item 2",
                Cipher = "Cipher 2",
                IsExt = 0,
                EffectiveDateFrom = new DateTime(2022, 01, 03),
                EffectiveDateBy = new DateTime(2022, 01, 10)
            },
            new()
            {
                Id = null,
                ExitID = 4,
                IsExt = 1,
                Name = "Item 3",
                Cipher = "Cipher 3",
                EffectiveDateFrom = new DateTime(2022, 01, 03),
                EffectiveDateBy = new DateTime(2022, 01, 10)
            }
        };
    }
}