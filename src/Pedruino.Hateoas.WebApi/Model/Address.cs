namespace Pedruino.Hateoas.WebApi.Model;

public class Address
{
    public int Id { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }
    public Person Person { get; set; }
}