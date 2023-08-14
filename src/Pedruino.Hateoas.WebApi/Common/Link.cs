using System.Text.Json.Serialization;

namespace Pedruino.Hateoas.WebApi.Common;

public class Link
{
    [JsonPropertyName("rel")]
    public string Rel { get; set; }
    
    [JsonPropertyName("href")]
    public string Href { get; set; }

    [JsonIgnore]
    public string ActionName { get; set; }
}