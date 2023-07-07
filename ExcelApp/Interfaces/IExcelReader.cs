using ExcelApp.Data;
using Microsoft.AspNetCore.Components.Forms;

namespace ExcelApp.Interfaces;

public interface IExcelReader
{
    Task<List<ProductInfo>> ReadExcelFileAsync(IBrowserFile file, IExcelDataFilter excelFilter);

    Task<List<ProductInfo>> ReadExcelFileAsync(IFormFile file, IExcelDataFilter excelFilter);
}