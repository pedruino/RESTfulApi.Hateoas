using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace Pedruino.Hateoas.WebApi.Common;

public sealed class PageParameters
{
    public const int MaxPageSize = 100;
    public const int DefaultPageNumber = 1;
    public const int DefaultPageSize = 15;

    public const string PageNumberKey = "page_number";
    public const string PageSizeKey = "page_size";

    [DataMember(Name = PageNumberKey)]
    [FromQuery(Name = PageNumberKey)]
    [Range(1, int.MaxValue, ErrorMessage = "Offset must be greater than 0.")]
    [JsonPropertyName(PageNumberKey)]
    public int PageNumber { get; set; } = DefaultPageNumber;

    [DataMember(Name = PageSizeKey)]
    [FromQuery(Name = PageSizeKey)]
    [Range(1, MaxPageSize, ErrorMessage = "Limit must be greater than 0 and less than 100.")]
    [JsonPropertyName(PageSizeKey)]
    public int PageSize { get; set; } = DefaultPageSize;
}