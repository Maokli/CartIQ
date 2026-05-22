namespace CartIQ.Application.Common.Models;

public class PaginationResult<T>
{
    public int PageIndex { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; }
    public int ItemsCount { get; set; }
    public List<T> Items { get; set; }

    public PaginationResult(int pageIndex, int totalPages, int pageSize, int itemsCount, List<T> items)
    {
        PageIndex = pageIndex;
        TotalPages = totalPages;
        PageSize = pageSize;
        ItemsCount = itemsCount;
        Items = items;
    }
}