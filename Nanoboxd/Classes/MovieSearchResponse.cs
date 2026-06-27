using Classes;
using System.Text.Json.Serialization;

namespace Services;

public class MovieSearchResponse
{
    [JsonPropertyName("results")]
    public List<Movie> Results { get; set; } = [];
}

