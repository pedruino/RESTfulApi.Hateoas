namespace Pedruino.Hateoas.WebApi.Dto;

public sealed class PersonDto : ResourceDto
{
    public string Name { get; set; }
    public string Document { get; set; }
}