using ExcelApp.Data;

namespace ExcelApp.Interfaces;

public interface IExcelDataFilter
{
    bool IsDataValid(ProductInfo productInfo);
}