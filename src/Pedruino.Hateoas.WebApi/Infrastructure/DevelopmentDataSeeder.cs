using Bogus;
using Bogus.Extensions.Brazil;
using Pedruino.Hateoas.WebApi.Model;
using Person = Pedruino.Hateoas.WebApi.Model.Person;

namespace Pedruino.Hateoas.WebApi.Infrastructure;

public static class DevelopmentDataSeeder
{
    public static void Seed(AppDbContext dbContext)
    {
        var people = FakeDataGenerator.GeneratePerson(100);
        dbContext.Set<Person>().AddRange(people);
        dbContext.SaveChanges();
    }
}

public static class FakeDataGenerator
{
    public static IEnumerable<Person> GeneratePerson(int count)
    {
        var addressFaker = new Faker<Address>()
                .RuleFor(a => a.Id, f => f.UniqueIndex)
                .RuleFor(a => a.Street, f => f.Address.StreetAddress())
                .RuleFor(a => a.City, f => f.Address.City())
                .RuleFor(a => a.State, f => f.Address.State())
                .RuleFor(a => a.ZipCode, f => f.Address.ZipCode())
            ;

        var phoneFaker = new Faker<Phone>()
                .RuleFor(p => p.Id, f => f.UniqueIndex)
                .RuleFor(p => p.Number, f => f.Phone.PhoneNumber())
            ;

        var productFaker = new Faker<Product>()
                .RuleFor(p => p.Id, f => Guid.NewGuid())
                .RuleFor(p => p.Name, f => f.Commerce.Product())
                .RuleFor(p => p.Price, f => Convert.ToDecimal(f.Commerce.Price()))
            ;

        var personFaker = new Faker<Person>("pt_BR")
                .RuleFor(p => p.Id, f => f.UniqueIndex)
                .RuleFor(p => p.Name, f => f.Person.FullName)
                .RuleFor(p => p.Document, f => f.Person.Cpf())
                .RuleFor(p => p.Addresses, f => addressFaker.Generate(f.Random.Int(1, 10)))
                .RuleFor(p => p.Phones, f => phoneFaker.Generate(f.Random.Int(1, 10)))
                .RuleFor(p => p.Products, f => productFaker.Generate(f.Random.Int(1, 100)))
            ;

        return personFaker.Generate(count);
    }
}