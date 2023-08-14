using System.Text.Json.Serialization;
using Pedruino.Hateoas.WebApi.Common;

namespace Pedruino.Hateoas.WebApi.Dto;

public abstract class ResourceDto
{
    [JsonPropertyName("_links")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyOrder(-1)]
    public Link[] Links { get; set; }
}