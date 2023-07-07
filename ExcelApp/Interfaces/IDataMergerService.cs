using ExcelApp.Data;

namespace ExcelApp.Interfaces;

public interface IDataMergerService
{
    public IEnumerable<ProductInfo> GetMergeData(List<ProductInfo> mainData, List<ProductInfo> dataForMerge);
}