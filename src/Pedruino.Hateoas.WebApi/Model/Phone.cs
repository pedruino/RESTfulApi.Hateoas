namespace Pedruino.Hateoas.WebApi.Model;

public class Phone
{
    public int Id { get; set; }
    public string Number { get; set; }
    public Person Person { get; set; }
}