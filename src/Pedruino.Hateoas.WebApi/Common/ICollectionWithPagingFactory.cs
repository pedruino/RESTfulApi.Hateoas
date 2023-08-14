namespace Pedruino.Hateoas.WebApi.Common;

public interface ICollectionWithPagingFactory
{
    CollectionWithPaging<T> Create<T>(Page<T> page, PageParameters pageParameters, Link self, IEnumerable<Link>? links = null);
}