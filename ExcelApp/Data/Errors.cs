namespace ExcelApp.Data;

public static class Errors
{
    public static readonly Dictionary<ErrorCode, string> ErrorDictionary = new()
    {
        { ErrorCode.Default, "Something went wrong." },
        { ErrorCode.StartDateIsWrong, "End date cannot be earlier than the start date." },
        { ErrorCode.DatNotFound, "Data not found" },
        { ErrorCode.InvalidFileFormatXlsx, "Invalid file format: {0}. Only .xlsx files are allowed." }

    };
}