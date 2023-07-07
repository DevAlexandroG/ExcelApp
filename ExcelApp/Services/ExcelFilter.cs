using ExcelApp.Data;
using ExcelApp.Interfaces;

namespace ExcelApp.Services;

/// <summary>
/// The ExcelFilter provides functionality for filtering data based on effective date criteria.
/// </summary>
public class ExcelFilter : IExcelDataFilter
{
    private readonly DateTime? _effectiveDateFrom;
    private readonly DateTime? _effectiveDateBy;

    /// <summary>
    /// Initializes a new instance of the ExcelFilter class with the specified effective date range.
    /// </summary>
    /// <param name="effectiveDateFrom">The starting date of the effective date range.</param>
    /// <param name="effectiveDateBy">The ending date of the effective date range.</param>
    public ExcelFilter(DateTime? effectiveDateFrom, DateTime? effectiveDateBy)
    {
        _effectiveDateFrom = effectiveDateFrom;
        _effectiveDateBy = effectiveDateBy;
    }

    /// <summary>
    /// Determines whether the given ProductInfo object's effective date falls within the specified date range.
    /// </summary>
    /// <param name="productInfo">The ProductInfo object to be checked.</param>
    /// <returns>True if the data is valid based on the effective date range; otherwise, false.</returns>
    public bool IsDataValid(ProductInfo productInfo)
    {
        return IsStartDateCorrect(productInfo.EffectiveDateFrom, _effectiveDateFrom)
               && IsEndDateCorrect(productInfo.EffectiveDateBy, _effectiveDateBy);
    }

    private static bool IsStartDateCorrect(DateTime? dateTimeFromExcel, DateTime? excelFilter)
    {
        return excelFilter == null || excelFilter <= dateTimeFromExcel;
    }

    private static bool IsEndDateCorrect(DateTime? dateTimeFromExcel, DateTime? excelFilter)
    {
        return excelFilter == null || excelFilter >= dateTimeFromExcel;
    }
}