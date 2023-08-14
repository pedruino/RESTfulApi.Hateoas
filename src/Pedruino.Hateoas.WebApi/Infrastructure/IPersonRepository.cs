using Pedruino.Hateoas.WebApi.Common;
using Pedruino.Hateoas.WebApi.Model;

namespace Pedruino.Hateoas.WebApi.Infrastructure;

public interface IPersonRepository
{
    Task<Person?> GetById(int id, CancellationToken cancellationToken);
    Task<Person?> GetByDocument(string document, CancellationToken cancellationToken);
    Task<Page<Person>> GetAll(PageParameters pageParameters, CancellationToken cancellationToken);
    Task<Page<Address>> GetAddresses(string document, PageParameters pageParameters, CancellationToken cancellationToken);
    Task<Page<Phone>> GetPhones(string document, PageParameters pageParameters, CancellationToken cancellationToken);
    Task<Page<Product>> GetProducts(string document, PageParameters pageParameters, CancellationToken cancellationToken);
}