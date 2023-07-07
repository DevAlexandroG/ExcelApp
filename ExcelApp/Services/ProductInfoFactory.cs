using ExcelApp.Data;
using OfficeOpenXml;

namespace ExcelApp.Services;

/// <summary>
/// The ProductInfoFactory class provides methods for creating ProductInfo objects from ExcelWorksheet data.
/// </summary>
public class ProductInfoFactory
{
    /// <summary>
    /// Creates a new ProductInfo object using the values from the specified ExcelWorksheet and row.
    /// </summary>
    /// <param name="worksheet">The ExcelWorksheet containing the data.</param>
    /// <param name="row">The row number in the ExcelWorksheet.</param>
    /// <returns>A new ProductInfo object.</returns>
    public ProductInfo CreateProductInfo(ExcelWorksheet worksheet, int row)
    {
        return new ProductInfo
        {
            Id = ParseIntValue(worksheet.Cells[row, 1].Value),
            Name = ParseStringValue(worksheet.Cells[row, 2].Value),
            Cipher = ParseStringValue(worksheet.Cells[row, 3].Value),
            EffectiveDateFrom = ParseDateTimeValue(worksheet.Cells[row, 4].Value),
            EffectiveDateBy = ParseDateTimeValue(worksheet.Cells[row, 5].Value),
        };
    }

    private int? ParseIntValue(object value)
    {
        return int.TryParse(value?.ToString(), out int result) ? result : null;
    }

    private string ParseStringValue(object value)
    {
        return value?.ToString();
    }

    private DateTime? ParseDateTimeValue(object value)
    {
        return DateTime.TryParse(value?.ToString(), out DateTime result) ? result : null;
    }
}