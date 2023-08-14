namespace Pedruino.Hateoas.WebApi.Model;

public class Person
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Document { get; set; }
    public ICollection<Address> Addresses { get; set; }
    public ICollection<Phone> Phones { get; set; }
    public ICollection<Product> Products { get; set; }
}