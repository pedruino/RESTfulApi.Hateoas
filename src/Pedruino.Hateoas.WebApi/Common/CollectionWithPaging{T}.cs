using System.Text.Json.Serialization;

namespace Pedruino.Hateoas.WebApi.Common;

public class CollectionWithPaging<TResouce>
{
    [JsonPropertyName("pagination")]
    [JsonPropertyOrder(1)]
    public Pagination Pagination { get; set; }
    
    [JsonPropertyName("data")]
    [JsonPropertyOrder(3)]
    public TResouce[] Data { get; set; }
    
    [JsonPropertyName("_links")]
    [JsonPropertyOrder(2)]
    public Link?[] Links { get; set; }
}