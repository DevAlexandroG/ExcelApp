namespace ExcelApp.Interfaces;

public interface IResultWrapper
{
    public bool IsSuccess { get; }

    public Exception Exception { get; }

    public string ExceptionMessage { get; }
}