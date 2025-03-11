namespace MovieManagement.Web.Common;

public class PagedList<T>(IEnumerable<T> items, int page, int pageSize, int totalCount)
{
    public int Page { get; } = page;
    public int PageSize { get; } = pageSize;
    public int TotalCount { get; } = totalCount;
    public IReadOnlyCollection<T> Items { get; } = items.ToList();
    public bool HasPreviousPage => Page > 1;
    public bool HasNextPage => Page * PageSize < TotalCount;
}
