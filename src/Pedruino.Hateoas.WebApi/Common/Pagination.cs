using System.Text.Json.Serialization;

namespace Pedruino.Hateoas.WebApi.Common;

public class Pagination
{
    [JsonPropertyOrder(1)] public int? CurrentPage { get; set; }

    [JsonPropertyOrder(2)] public int? PageSize { get; set; }

    [JsonPropertyOrder(3)] public long TotalItems { get; set; }

    [JsonPropertyOrder(4)] public Link? Previous { get; set; }

    [JsonPropertyOrder(5)] public Link? Next { get; set; }
}