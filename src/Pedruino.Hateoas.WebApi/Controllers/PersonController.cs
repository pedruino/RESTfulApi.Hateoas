using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Pedruino.Hateoas.WebApi.Common;
using Pedruino.Hateoas.WebApi.Dto;
using Pedruino.Hateoas.WebApi.Infrastructure;

namespace Pedruino.Hateoas.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PersonController : ControllerBase
{
    private readonly ICollectionWithPagingFactory _collectionWithPagingFactory;
    private readonly ILinkService _linkService;
    private readonly IMapper _mapper;
    private readonly IPersonRepository _personRepository;
    private const string PersonRelationAll = "";
    private const string PersonRelationAddresses = "addresses";
    private const string PersonRelationPhones = "phones";
    private const string PersonRelationProducts = "products";

    public PersonController(IPersonRepository personRepository, IMapper mapper, ILinkService linkService,
        ICollectionWithPagingFactory collectionWithPagingFactory)
    {
        _personRepository = personRepository;
        _mapper = mapper;
        _linkService = linkService;
        _collectionWithPagingFactory = collectionWithPagingFactory;
    }

    [HttpGet($"{PersonRelationAll}")]
    public async Task<IActionResult> GetPeople([FromQuery] PageParameters pageParameters, CancellationToken ct)
    {
        var page = await _personRepository.GetAll(pageParameters, ct);
        var pageDto = _mapper.Map<Page<PersonDto>>(page);
        foreach (var personDto in pageDto.Items)
        {
            personDto.Links = GetPersonLinks(personDto);
        }

        var self = _linkService.CreateSelfLink(nameof(GetPeople));
        var collection = _collectionWithPagingFactory.Create(pageDto, pageParameters, self);

        return Ok(collection);
    }

    [HttpGet("{document}")]
    public async Task<IActionResult> GetPersonData(string document, CancellationToken ct)
    {
        var person = _mapper.Map<PersonDto>(await _personRepository.GetByDocument(document, ct));
        if (person == null) return NotFound();

        person.Links = GetPersonLinks(person);

        return Ok(person);
    }

    private Link[] GetPersonLinks(PersonDto person)
    {
        var linkParameters = new { document = person.Document };
        var innerSelf = _linkService.CreateSelfLink(nameof(GetPersonData), linkParameters);
        var addresses = _linkService.CreateLink(PersonRelationAddresses, nameof(GetAddresses), linkParameters);
        var phones = _linkService.CreateLink(PersonRelationPhones, nameof(GetPhones), linkParameters);
        var products = _linkService.CreateLink(PersonRelationProducts, nameof(GetProducts), linkParameters);

        var links = new[] { innerSelf, addresses, phones, products };
        return links;
    }

    [HttpGet($"{{document}}/{PersonRelationAddresses}")]
    public async Task<IActionResult> GetAddresses(string document, [FromQuery] PageParameters pageParameters,
        CancellationToken ct)
    {
        var page = await _personRepository.GetAddresses(document, pageParameters, ct);
        var pageDto = _mapper.Map<Page<AddressDto>>(page);
        var self = _linkService.CreateSelfLink(nameof(GetAddresses), new { document });
        var collection = _collectionWithPagingFactory.Create(pageDto, pageParameters, self);
        return Ok(collection);
    }

    [HttpGet($"{{document}}/{PersonRelationPhones}")]
    public async Task<IActionResult> GetPhones(string document, [FromQuery] PageParameters pageParameters,
        CancellationToken ct)
    {
        var page = await _personRepository.GetPhones(document, pageParameters, ct);
        var pageDto = _mapper.Map<Page<PhoneDto>>(page);
        var self = _linkService.CreateSelfLink(nameof(GetPhones), new { document });
        var collection = _collectionWithPagingFactory.Create(pageDto, pageParameters, self);
        return Ok(collection);
    }

    [HttpGet($"{{document}}/{PersonRelationProducts}")]
    public async Task<IActionResult> GetProducts(string document, [FromQuery] PageParameters pageParameters,
        CancellationToken ct)
    {
        var page = await _personRepository.GetProducts(document, pageParameters, ct);
        var pageDto = _mapper.Map<Page<ProductDto>>(page);
        var self = _linkService.CreateSelfLink(nameof(GetProducts), new { document });
        var collection = _collectionWithPagingFactory.Create(pageDto, pageParameters, self);
        return Ok(collection);
    }
}