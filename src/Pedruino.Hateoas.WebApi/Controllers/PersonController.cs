using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pedruino.Hateoas.WebApi.Common;
using Pedruino.Hateoas.WebApi.Dto;
using Pedruino.Hateoas.WebApi.Infrastructure;

namespace Pedruino.Hateoas.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PersonController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IPersonRepository _personRepository;
    private readonly ILinkService _linkService;
    private readonly ICollectionWithPagingFactory _collectionWithPagingFactory;

    public PersonController(IPersonRepository personRepository, IMapper mapper, ILinkService linkService, ICollectionWithPagingFactory collectionWithPagingFactory)
    {
        _personRepository = personRepository;
        _mapper = mapper;
        _linkService = linkService;
        _collectionWithPagingFactory = collectionWithPagingFactory;
    }

    [HttpGet("")]
    public async Task<IActionResult> GetPeople([FromQuery] PageParameters pageParameters, CancellationToken ct)
    {
        var pagePeople = _mapper.Map<Page<PersonDto>>(await _personRepository.GetAll(pageParameters, ct));
        foreach (var personDto in pagePeople.Items)
        {
            var linkParameters = new { document = personDto.Document };
            var innerSelf = _linkService.CreateLink("self", nameof(GetPersonData), linkParameters);
            var addresses = _linkService.CreateLink("addresses", nameof(GetAddresses), linkParameters);
            var phones = _linkService.CreateLink("phones", nameof(GetPhones), linkParameters);
            var products = _linkService.CreateLink("products", nameof(GetProducts), linkParameters);

            personDto.Links = new[] { innerSelf, addresses, phones, products };
        }

        var self = _linkService.CreateLink(CollectionWithPagingFactory.SelfRelation, nameof(GetPeople));
        var collection = _collectionWithPagingFactory.Create(pagePeople, pageParameters, self);

        return Ok(collection);
    }

    [HttpGet("{document}")]
    public async Task<IActionResult> GetPersonData(string document, CancellationToken ct)
    {
        var person = _mapper.Map<PersonDto>(await _personRepository.GetByDocument(document, ct));
        if (person == null) return NotFound();
        
        person.Links = InnerLinks(person);

        return Ok(person);
    }

    private Link[] InnerLinks(PersonDto person)
    {
        var linkParameters = new { document = person.Document };
        var innerSelf = _linkService.CreateLink("self", nameof(GetPersonData), linkParameters);
        var addresses = _linkService.CreateLink("addresses", nameof(GetAddresses), linkParameters);
        var phones = _linkService.CreateLink("phones", nameof(GetPhones), linkParameters);
        var products = _linkService.CreateLink("products", nameof(GetProducts), linkParameters);

        var links = new[] { innerSelf, addresses, phones, products };
        return links;
    }

    [HttpGet("{document}/addresses")]
    public async Task<IActionResult> GetAddresses(string document, [FromQuery] PageParameters pageParameters, CancellationToken ct)
    {
        var addresses = _mapper.Map<Page<AddressDto>>(await _personRepository.GetAddresses(document, pageParameters, ct));
        var linkParameters = new { document = document };
        var self = _linkService.CreateLink(CollectionWithPagingFactory.SelfRelation, nameof(GetAddresses), linkParameters);
        var collection = _collectionWithPagingFactory.Create(addresses, pageParameters, self);
        return Ok(collection);
    }
    
    [HttpGet("{document}/phones")]
    public async Task<IActionResult> GetPhones(string document, [FromQuery] PageParameters pageParameters, CancellationToken ct)
    {
        var phones = _mapper.Map<Page<PhoneDto>>(await _personRepository.GetPhones(document, pageParameters, ct));
        var linkParameters = new { document = document };
        var self = _linkService.CreateLink(CollectionWithPagingFactory.SelfRelation, nameof(GetPhones), linkParameters);
        var collection = _collectionWithPagingFactory.Create(phones, pageParameters, self);
        return Ok(collection);
    }
    
    [HttpGet("{document}/products")]
    public async Task<IActionResult> GetProducts(string document, [FromQuery] PageParameters pageParameters, CancellationToken ct)
    {
        var products = _mapper.Map<Page<ProdutctDto>>(await _personRepository.GetProducts(document, pageParameters, ct));
        var linkParameters = new { document = document };
        var self = _linkService.CreateLink(CollectionWithPagingFactory.SelfRelation, nameof(GetProducts), linkParameters);
        var collection = _collectionWithPagingFactory.Create(products, pageParameters, self);
        return Ok(collection);
    }
}