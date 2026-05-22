namespace CartIQ.Application.Common.Models;

public class Result<T>
{
    public bool Succeeded { get; init; }
    public string[] Errors { get; init; }   
    public T ResultItem { get; private set; }
    
    internal Result(IEnumerable<string> errors)
    {
        Succeeded = false;
        ResultItem = default;
        Errors = errors.ToArray();
    }
    
    internal Result(bool succeeded, IEnumerable<string> errors, T resultItem)
    {
        Succeeded = succeeded;
        ResultItem = resultItem;
        Errors = errors.ToArray();
    }
    
    public static Result<T> Success(T resultItem)
    {
        return new Result<T>(true, Array.Empty<string>(), resultItem: resultItem);
    }

    public static Result<T> Failure(IEnumerable<string> errors)
    {
        return new Result<T>(errors);
    }
}