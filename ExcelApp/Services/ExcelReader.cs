using ExcelApp.Data;
using ExcelApp.Interfaces;
using Microsoft.AspNetCore.Components.Forms;
using OfficeOpenXml;

namespace ExcelApp.Services;

/// <summary>
/// The ExcelReader provides functionality for reading Excel files and extracting ProductInfo objects.
/// </summary>
public class ExcelReader : IExcelReader
{
    private readonly ProductInfoFactory _productInfoFactory;

    private const int MinRow = 1;
    private const int StartRowForReadingData = 2;

    /// <summary>
    /// Initializes a new instance of the ExcelReader class with the specified ProductInfoFactory.
    /// </summary>
    /// <param name="productInfoFactory">The factory responsible for creating ProductInfo objects.</param>
    public ExcelReader(ProductInfoFactory productInfoFactory)
    {
        _productInfoFactory = productInfoFactory;
    }

    /// <summary>
    /// Reads an Excel file asynchronously and extracts a list of ProductInfo objects based on the provided Excel data filter.
    /// </summary>
    /// <param name="file">The IBrowserFile representing the Excel file.</param>
    /// <param name="excelFilter">The filter to be applied to the Excel data.</param>
    /// <returns>A Task representing the asynchronous operation that returns a list of ProductInfo objects.</returns>
    public async Task<List<ProductInfo>> ReadExcelFileAsync(IBrowserFile file, IExcelDataFilter excelFilter)
    {
        using var stream = new MemoryStream();
        await file.OpenReadStream().CopyToAsync(stream);
        return await ReadExcelFileAsync(stream, excelFilter);
    }

    /// <summary>
    /// Reads an Excel file asynchronously and extracts a list of ProductInfo objects based on the provided Excel data filter.
    /// </summary>
    /// <param name="file">The IFormFile representing the Excel file.</param>
    /// <param name="excelFilter">The filter to be applied to the Excel data.</param>
    /// <returns>A Task representing the asynchronous operation that returns a list of ProductInfo objects.</returns>
    public async Task<List<ProductInfo>> ReadExcelFileAsync(IFormFile file, IExcelDataFilter excelFilter)
    {
        using var stream = new MemoryStream();
        await file.OpenReadStream().CopyToAsync(stream);
        return await ReadExcelFileAsync(stream, excelFilter);
    }

    private async Task<List<ProductInfo>> ReadExcelFileAsync(Stream stream, IExcelDataFilter excelFilter)
    {
        var productInfos = new List<ProductInfo>();
        using var package = new ExcelPackage(stream);
        var worksheet = package.Workbook.Worksheets.FirstOrDefault();
        if (worksheet == null) return productInfos;

        var lastRow = worksheet.Cells
            .Where(cell => cell.Start.Row > MinRow)
            .LastOrDefault(cell => !string.IsNullOrEmpty(cell.Value?.ToString()));

        var rowCount = lastRow?.End.Row ?? MinRow;

        for (var row = StartRowForReadingData; row <= rowCount; row++)
        {
            var productInfo = _productInfoFactory.CreateProductInfo(worksheet, row);
            if (excelFilter.IsDataValid(productInfo))
                productInfos.Add(productInfo);
        }

        return productInfos;
    }
}