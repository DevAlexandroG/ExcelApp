using ExcelApp.Data;
using ExcelApp.Interfaces;

namespace ExcelApp.Helpers;

public class ResultWrapper<T> : IResultWrapper
{
    public bool IsSuccess { get; private set; }
    public T Value { get; private set; }
    public Exception Exception { get; private set; }

    public string ExceptionMessage { get; private set; }

    /// <summary>
    /// Creates a failure ResultWrapper object based on the provided IResultWrapper.
    /// </summary>
    /// <param name="resultWrapper">The IResultWrapper object containing the failure information.</param>
    /// <returns>A new ResultWrapper object indicating failure.</returns>
    public static ResultWrapper<T> Failure(IResultWrapper resultWrapper)
    {
        return new ResultWrapper<T>()
        {
            IsSuccess = false,
            Exception = resultWrapper.Exception,
            ExceptionMessage = resultWrapper.ExceptionMessage
        };
    }

    /// <summary>
    /// Creates a failure ResultWrapper object with the provided exception message.
    /// </summary>
    /// <param name="message">The exception message.</param>
    /// <returns>A new ResultWrapper object indicating failure.</returns>
    public static ResultWrapper<T> Failure(string message)
    {
        return new ResultWrapper<T>()
        {
            IsSuccess = false,
            ExceptionMessage = message
        };
    }

    /// <summary>
    /// Creates a success ResultWrapper object with the provided value.
    /// </summary>
    /// <param name="value">The value to be wrapped.</param>
    /// <returns>A new ResultWrapper object indicating success.</returns>
    public static ResultWrapper<T> Success(T value)
    {
        return new ResultWrapper<T>()
        {
            IsSuccess = true,
            Value = value
        };
    }

    /// <summary>
    /// Executes the specified function and wraps the result in a ResultWrapper object.
    /// </summary>
    /// <param name="func">The function to be executed.</param>
    /// <returns>A ResultWrapper object containing the result of the function execution.</returns>
    public static ResultWrapper<T> Execute(Func<T> func)
    {
        var resultWrapper = new ResultWrapper<T>();
        try
        {
            resultWrapper.Value = func.Invoke();
            resultWrapper.IsSuccess = true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message); //Can be log
            resultWrapper.Exception = ex;
            resultWrapper.ExceptionMessage = Errors.ErrorDictionary[ErrorCode.Default];
            resultWrapper.IsSuccess = false;
        }

        return resultWrapper;
    }

    /// <summary>
    /// Executes the specified asynchronous function and wraps the result in a ResultWrapper object.
    /// </summary>
    /// <param name="func">The asynchronous function to be executed.</param>
    /// <returns>A Task representing the asynchronous operation that returns a ResultWrapper object.</returns>
    public static async Task<ResultWrapper<T>> ExecuteAsync(Func<Task<T>> func)
    {
        var resultWrapper = new ResultWrapper<T>();

        try
        {
            resultWrapper.Value = await func.Invoke();
            resultWrapper.IsSuccess = true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message); //Can be log
            resultWrapper.Exception = ex;
            resultWrapper.ExceptionMessage = Errors.ErrorDictionary[ErrorCode.Default];
            resultWrapper.IsSuccess = false;
        }

        return resultWrapper;
    }
}