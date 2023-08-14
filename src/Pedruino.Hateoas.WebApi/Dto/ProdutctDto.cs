namespace Pedruino.Hateoas.WebApi.Dto;

public sealed class ProdutctDto : ResourceDto
{
    public string Name { get; set; }
    public decimal Price { get; set; }
}