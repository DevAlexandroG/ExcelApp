using ExcelApp.Data;

namespace ExcelApp.Helpers;

public class DataMerger
{
    private const int MainFile = 0;
    private const int ExtensionFile = 1;

    /// <summary>
    /// Merges the mainData list with the dataForMerge list and returns the merged result.
    /// </summary>
    /// <param name="mainData">The main data list.</param>
    /// <param name="dataForMerge">The data to be merged.</param>
    /// <returns>An IEnumerable of ProductInfo representing the merged data.</returns>
    public IEnumerable<ProductInfo> MergeData(List<ProductInfo> mainData, List<ProductInfo> dataForMerge)
    {
        var mainDataLookup = mainData.ToDictionary(item => item.Cipher);
        var resultData = new List<ProductInfo>();

        foreach (var mergeData in dataForMerge)
        {
            if (mainDataLookup.TryGetValue(mergeData.Cipher, out var myItem))
            {
                myItem.ExitID = mergeData.Id;
                myItem.EffectiveDateBy = GetLaterDateOrNull(myItem.EffectiveDateBy, mergeData.EffectiveDateBy);
                myItem.EffectiveDateFrom = GetEarlyDateOrNull(myItem.EffectiveDateFrom, mergeData.EffectiveDateFrom);
                myItem.IsExt = MainFile;
            }
            else
            {
                myItem = CreateNewMyItem(mergeData);
                mainData.Add(myItem);
            }

            resultData.Add(myItem);
        }

        var unmatchedItems = mainData
            .Where(item => dataForMerge.All(d => d.Cipher != item.Cipher))
            .Select(CreateUnmatchedMyItem);
        resultData.AddRange(unmatchedItems);

        return resultData;
    }

    private static ProductInfo CreateNewMyItem(ProductInfo mergeData)
    {
        return new ProductInfo
        {
            Id = null, //Math.Abs(Guid.NewGuid().GetHashCode()),
            ExitID = mergeData.Id,
            Name = mergeData.Name,
            IsExt = ExtensionFile,
            Cipher = mergeData.Cipher,
            EffectiveDateBy = mergeData.EffectiveDateBy,
            EffectiveDateFrom = mergeData.EffectiveDateFrom
        };
    }

    private static ProductInfo CreateUnmatchedMyItem(ProductInfo item)
    {
        return new ProductInfo()
        {
            Id = item.Id,
            Name = item.Name,
            Cipher = item.Cipher,
            EffectiveDateBy = item.EffectiveDateBy,
            EffectiveDateFrom = item.EffectiveDateFrom,
            IsExt = MainFile
        };
    }

    private static DateTime? GetLaterDateOrNull(DateTime? date1, DateTime? date2)
    {
        return (date1.HasValue && date2.HasValue) ? (date1 > date2 ? date1 : date2) : (date1 ?? date2);
    }

    private static DateTime? GetEarlyDateOrNull(DateTime? date1, DateTime? date2)
    {
        return (date1.HasValue && date2.HasValue) ? (date1 < date2 ? date1 : date2) : (date1 ?? date2);
    }
}