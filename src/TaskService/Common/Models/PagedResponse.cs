namespace TaskService.Common.Models
{
  public sealed class PagedResponse<T>
  {
    public List<T> Items { get; init; } = [];
    public int CurrentPage { get; init; }
    public int PageSize { get; init; }
    public int TotalCount { get; init; }
    public int TotalPages { get; init; }
  }
}