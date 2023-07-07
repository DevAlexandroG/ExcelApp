using ExcelApp.Data;
using ExcelApp.Helpers;
using ExcelApp.Interfaces;

namespace ExcelApp.Services;

using System.Collections.Generic;

/// <summary>
/// The DataMergerService class represents a service that handles data merging operations.
/// </summary>
/// <remarks>
/// This class encapsulates the logic for merging data by utilizing the DataMerger class.
/// </remarks>
public class DataMergerService : IDataMergerService
{
    private readonly DataMerger _dataMerger;

    /// <summary>
    /// Initializes a new instance of the DataMergerService class.
    /// </summary>
    public DataMergerService()
    {
        _dataMerger = new DataMerger();
    }

    /// <summary>
    /// Retrieves the merged data by combining the mainData and dataForMerge lists.
    /// </summary>
    /// <param name="mainData">The main data list.</param>
    /// <param name="dataForMerge">The data to be merged.</param>
    /// <returns>The merged data as an collection ProductInfo.</returns>
    public IEnumerable<ProductInfo> GetMergeData(List<ProductInfo> mainData, List<ProductInfo> dataForMerge)
    {
        return _dataMerger.MergeData(mainData, dataForMerge);
    }
}