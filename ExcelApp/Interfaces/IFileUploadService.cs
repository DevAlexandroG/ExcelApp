using ExcelApp.Data;
using ExcelApp.Helpers;
using ExcelApp.Services;
using Microsoft.AspNetCore.Components.Forms;

namespace ExcelApp.Interfaces;

public interface IFileUploadService
{
    Task<ResultWrapper<IEnumerable<ProductInfo>>> UploadFile(List<IBrowserFile> browserFiles, ExcelFilter excelFilter);
}