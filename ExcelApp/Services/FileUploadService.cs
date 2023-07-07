using ExcelApp.Data;
using ExcelApp.Helpers;
using ExcelApp.Interfaces;
using Microsoft.AspNetCore.Components.Forms;

namespace ExcelApp.Services;

/// <summary>
/// The FileUploadService provides functionality for uploading and processing files.
/// </summary>
public class FileUploadService : IFileUploadService
{
    private readonly IDataMergerService _dataMergerService;
    private readonly IExcelReader _excelReader;
    private readonly IFileFormatValidator _fileFormatValidator;
    private const int RequiredCountElementsForMerge = 2;

    public FileUploadService(
        IDataMergerService dataMergerService,
        IExcelReader excelReader,
        IFileFormatValidator fileFormatValidator)
    {
        _dataMergerService = dataMergerService;
        _excelReader = excelReader;
        _fileFormatValidator = fileFormatValidator;
    }

    /// <summary>
    /// Uploads multiple files asynchronously and performs data merging based on the provided Excel filter.
    /// </summary>
    /// <param name="browserFiles">The list of IBrowserFile objects representing the uploaded files.</param>
    /// <param name="excelFilter">The filter to be applied to the Excel data.</param>
    /// <returns>A Task representing the asynchronous operation that returns a ResultWrapper containing the merged ProductInfo data.</returns>
    public async Task<ResultWrapper<IEnumerable<ProductInfo>>> UploadFile(List<IBrowserFile> browserFiles,
        ExcelFilter excelFilter)
    {
        var invalidFiles = browserFiles.Where(file => !_fileFormatValidator.IsValidFileFormat(file)).ToList();
        if (invalidFiles.Any())
        {
            var invalidFileNames = string.Join(", ", invalidFiles.Select(file => file.Name));
            var errorMessage = string.Format(Errors.ErrorDictionary[ErrorCode.InvalidFileFormatXlsx], invalidFileNames);

            return ResultWrapper<IEnumerable<ProductInfo>>.Failure(errorMessage);
        }

        var toMergeResult =
            await ResultWrapper<List<List<ProductInfo>>>.ExecuteAsync(() => ReadFilesAsync(browserFiles, excelFilter));

        if (toMergeResult is { IsSuccess: true, Value.Count: RequiredCountElementsForMerge })
        {
            var mergedDataResult = ResultWrapper<IEnumerable<ProductInfo>>.Execute(() =>
                MergeData(toMergeResult.Value[0], toMergeResult.Value[1]));

            return !mergedDataResult.Value.Any()
                ? ResultWrapper<IEnumerable<ProductInfo>>
                    .Failure(Errors.ErrorDictionary[ErrorCode.DatNotFound])
                : mergedDataResult;
        }


        return ResultWrapper<IEnumerable<ProductInfo>>.Failure(toMergeResult);
    }

    private async Task<List<List<ProductInfo>>> ReadFilesAsync(List<IBrowserFile> browserFiles, ExcelFilter excelFilter)
    {
        var toMerge = new List<List<ProductInfo>>();
        foreach (var file in browserFiles)
        {
            var newList = await _excelReader.ReadExcelFileAsync(file, excelFilter);
            toMerge.Add(newList);
        }

        return toMerge;
    }

    private IEnumerable<ProductInfo> MergeData(List<ProductInfo> mainData, List<ProductInfo> dataForMerge)
    {
        return _dataMergerService.GetMergeData(mainData, dataForMerge);
    }
}