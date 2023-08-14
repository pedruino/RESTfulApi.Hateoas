namespace Pedruino.Hateoas.WebApi.Dto;

public sealed class AddressDto : ResourceDto
{
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }
}