namespace Pedruino.Hateoas.WebApi.Common;

public interface ILinkService
{
    Link CreateLink(string relation, string action, object? values = null);

    Link CreateSelfLink(string action, object? values = null);
}