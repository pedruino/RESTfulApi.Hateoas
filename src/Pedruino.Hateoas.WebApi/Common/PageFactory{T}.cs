using Microsoft.EntityFrameworkCore;

namespace Pedruino.Hateoas.WebApi.Common;

public static class PageFactory<T> where T : class
{
    public static async Task<Page<T>> CreateAsync(IQueryable<T> queryable, int pageNumber, int pageSize,
        CancellationToken cancellationToken)
    {
        var totalItems = await queryable.CountAsync(cancellationToken);
        var items = await Paginate(queryable, pageNumber, pageSize).ToListAsync(cancellationToken);

        return new Page<T>
        {
            CurrentPage = pageNumber,
            PageSize = pageSize,
            TotalItems = totalItems,
            HasNext = HasNext(pageSize, items),
            HasPrevious = pageNumber > 1,
            Items = items.Take(pageSize)
        };
    }

    private static IQueryable<TSource> Paginate<TSource>(IQueryable<TSource> source, int pageNumber, int pageSize) 
        => source.Skip(pageSize * (pageNumber - 1)).Take(pageSize + 1);

    private static bool HasNext(int pageSize, ICollection<T> items) => items.Count > pageSize;
}