
using System.Text.Json;
namespace Services;

public class TMDbServiceAPI
{
    private readonly HttpClient _httpClient = new();

    private static readonly string ApiKey = Environment.GetEnvironmentVariable("TMDB_API_KEY")
        ?? throw new InvalidOperationException("TMDB_API_KEY environment variable is not set.");

    public async Task<MovieSearchResponse> SearchMovie(string title)
    {
        string url =
            $"https://api.themoviedb.org/3/search/movie?query={title}&api_key={ApiKey}";

        string json = await _httpClient.GetStringAsync(url);

        return JsonSerializer.Deserialize<MovieSearchResponse>(json) ?? new MovieSearchResponse
        ();
    }

}