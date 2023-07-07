using ExcelApp.Interfaces;
using Microsoft.AspNetCore.Components.Forms;

namespace ExcelApp.Services;

public class XlsxFileFormatValidator : IFileFormatValidator
{
    public bool IsValidFileFormat(IBrowserFile file)
    {
        return file.Name.EndsWith(".xlsx");
    }
}