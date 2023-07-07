using Microsoft.AspNetCore.Components.Forms;

namespace ExcelApp.Interfaces;

public interface IFileFormatValidator
{
    bool IsValidFileFormat(IBrowserFile file);
}