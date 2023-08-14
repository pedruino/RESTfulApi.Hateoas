using Microsoft.EntityFrameworkCore;
using Pedruino.Hateoas.WebApi.Common;
using Pedruino.Hateoas.WebApi.Model;

namespace Pedruino.Hateoas.WebApi.Infrastructure;

public class PersonRepository : IPersonRepository
{
    private readonly AppDbContext _context;
    private readonly DbSet<Person> _personDbSet;

    public PersonRepository(AppDbContext dbContext)
    {
        _context = dbContext;
        _personDbSet = dbContext.Set<Person>();
    }

    public Task<Person?> GetById(int id, CancellationToken cancellationToken)
    {
        return _personDbSet.FirstOrDefaultAsync(person => person.Id == id, cancellationToken);
    }

    public Task<Person?> GetByDocument(string document, CancellationToken cancellationToken)
    {
        return _personDbSet.SingleOrDefaultAsync(person => person.Document == document, cancellationToken);
    }

    public async Task<Page<Person>> GetAll(PageParameters pageParameters, CancellationToken cancellationToken)
    {
        var query = _personDbSet;
        return await PageFactory<Person>.CreateAsync(query, pageParameters.PageNumber, pageParameters.PageSize,
            cancellationToken);
    }

    public async Task<Page<Address>> GetAddresses(string document, PageParameters pageParameters,
        CancellationToken cancellationToken)
    {
        var queryable = _context.Set<Person>().Include(person => person.Addresses);
        var query = queryable.Where(person => person.Document == document)
            .SelectMany(person => person.Addresses);
        return await PageFactory<Address>.CreateAsync(query, pageParameters.PageNumber, pageParameters.PageSize,
            cancellationToken);
    }

    public async Task<Page<Phone>> GetPhones(string document, PageParameters pageParameters,
        CancellationToken cancellationToken)
    {
        var queryable = _context.Set<Person>().Include(person => person.Phones);
        var query = queryable.Where(person => person.Document == document)
            .SelectMany(person => person.Phones);
        return await PageFactory<Phone>.CreateAsync(query, pageParameters.PageNumber, pageParameters.PageSize,
            cancellationToken);
    }

    public Task<Page<Product>> GetProducts(string document, PageParameters pageParameters,
        CancellationToken cancellationToken)
    {
        var queryable = _context.Set<Person>().Include(person => person.Products);
        var query = queryable.Where(person => person.Document == document)
            .SelectMany(person => person.Products);
        return PageFactory<Product>.CreateAsync(query, pageParameters.PageNumber, pageParameters.PageSize,
            cancellationToken);
    }
}