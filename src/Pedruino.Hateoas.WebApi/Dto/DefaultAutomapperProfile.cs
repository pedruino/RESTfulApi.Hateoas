using AutoMapper;
using Pedruino.Hateoas.WebApi.Common;
using Pedruino.Hateoas.WebApi.Model;

namespace Pedruino.Hateoas.WebApi.Dto;

public class DefaultAutomapperProfile : Profile
{
    public DefaultAutomapperProfile()
    {
        CreateMap<Person, PersonDto>();
        CreateMap<Address, AddressDto>();
        CreateMap<Phone, PhoneDto>();
        CreateMap<Product, ProdutctDto>();
        CreateMap(typeof(Page<>), typeof(Page<>));
    }
}