namespace Pedruino.Hateoas.WebApi.Common;

public interface IPage<T>
{
    int CurrentPage { get; set; }
    int PageSize { get; set; }
    IEnumerable<T> Items { get; set; }
    long TotalItems { get; set; }
}