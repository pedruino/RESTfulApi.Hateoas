using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;

namespace Pedruino.Hateoas.WebApi.Common;

public class LinkService : ILinkService
{
    private readonly IUrlHelper _urlHelper;

    public LinkService(IUrlHelperFactory urlHelperFactory, IActionContextAccessor actionContextAccessor)
    {
        _urlHelper = urlHelperFactory.GetUrlHelper(actionContextAccessor.ActionContext);
    }
    
    public Link CreateLink(string relation, string action, object? values = null)
    {
        return new Link
        {
            Rel = relation,
            Href = _urlHelper.ActionLink(action, values: new RouteValueDictionary(values))
        };
    }

    public Link CreateSelfLink(string action, object? values = null) 
        => CreateLink(Relation.Self, action, values);

    public static class Relation
    {
        public const string Self = "self";
        public const string Next = "next";
        public const string Previous = "previous";
    }
}