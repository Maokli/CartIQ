namespace CartIQ.Application.Common.Models;

public class Result<T>
{
    public T ResultItem { get; private set; }
    
    internal Result(T resultItem)
    {
        ResultItem = resultItem;
    }
    
    public static Result<T> Success(T resultItem)
    {
        return new Result<T>(resultItem: resultItem);
    }
}