namespace Pedruino.Hateoas.WebApi.Common;

public sealed class Page<T> : IPage<T>
{
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public long TotalItems { get; set; }
    public IEnumerable<T> Items { get; set; }
    public bool HasNext { get; set; }
    public bool HasPrevious { get; set; }
}