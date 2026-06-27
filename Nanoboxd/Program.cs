﻿using Classes;
using Services;
using System.Text.Json;

string banner = File.ReadAllText("Header.txt");

Console.WriteLine(banner);

Console.WriteLine("Please enter your username and password:");

User user = new User();

user.EnterCredentials();

Console.WriteLine($"Welcome, {user.Username}!");

TMDbServiceAPI tmdb = new TMDbServiceAPI();

while (true)
{
    Console.Write("\nSearch for a movie (or type 'exit' to quit): ");
    string input = Console.ReadLine() ?? string.Empty;

    if (input.ToLower() == "exit") break;
    if (string.IsNullOrWhiteSpace(input)) continue;

    MovieSearchResponse response = await tmdb.SearchMovie(input);

    if (response.Results.Count == 0)
    {
        Console.WriteLine("No movies found.");
        continue;
    }

    Console.WriteLine($"\nFound {response.Results.Count} result(s):\n");

    foreach (Movie movie in response.Results)
    {
        Console.WriteLine($"  [{movie.Id}] {movie.Title} ({movie.ReleaseDate}) — Rating: {movie.Rating:F1}/10");
    }
}
