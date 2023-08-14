namespace Pedruino.Hateoas.WebApi.Common;

public class CollectionWithPagingFactory : ICollectionWithPagingFactory
{
    private readonly ILinkService _linkService;

    public CollectionWithPagingFactory(ILinkService linkService)
    {
        _linkService = linkService;
    }

    public CollectionWithPaging<T> Create<T>(Page<T> page, PageParameters pageParameters, Link self, IEnumerable<Link>? links = null)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));

        var paging = new CollectionWithPaging<T>
        {
            Pagination = new Pagination
            {
                CurrentPage = page.CurrentPage,
                PageSize = page.PageSize,
                TotalItems = page.TotalItems,
                Previous = GetPreviousLink(self, pageParameters, page.HasPrevious),
                Next = GetNextLink(self, pageParameters, page.HasNext)
            },
            Links = MergeLinks(self, links),
            Data = page.Items.ToArray()
        };

        return paging;
    }

    private static Link?[] MergeLinks(Link self, IEnumerable<Link>? links)
    {
        if (links == null) return new[] { self };
        
        var enumerable = links.ToList();
        return enumerable.Any() ? enumerable.Append(self).ToArray() : new[] { self };
    }


    private Link? GetNextLink(Link self, PageParameters pageParameters, bool condition)
    {
        if (!condition) return null;

        var pageSize = pageParameters.PageSize;
        var pageNumber = pageParameters.PageNumber + 1;

        var parameters = new RouteValueDictionary
        {
            [PageParameters.PageNumberKey] = pageNumber,
            [PageParameters.PageSizeKey] = pageSize
        };

        return NewLink(LinkService.Relation.Next, self, parameters);
    }
    
    private Link? GetPreviousLink(Link self, PageParameters pageParameters, bool condition)
    {
        if (!condition) return null;

        var pageSize = pageParameters.PageSize;
        var pageNumber = pageParameters.PageNumber - 1;

        var parameters = new RouteValueDictionary
        {
            [PageParameters.PageNumberKey] = pageNumber,
            [PageParameters.PageSizeKey] = pageSize
        };

        return NewLink(LinkService.Relation.Previous, self, parameters);
    }

    private Link NewLink(string relation, Link self, RouteValueDictionary parameters) 
        => _linkService.CreateLink(relation, self.ActionName, parameters);
}