﻿using Classes;
using Services;

string banner = File.ReadAllText("Header.txt");

Console.WriteLine(banner);

Console.WriteLine("Please create a  username and password");

User user = new User();

user.EnterCredentials();

Console.WriteLine("Thank you for creating an account! Please enter your credentials to log in.");

Console.WriteLine("Enter your username and password:");

Console.WriteLine("Username:");
string enteredUsername = Console.ReadLine() ?? string.Empty;

Console.WriteLine("Password:");
string enteredPassword = Console.ReadLine() ?? string.Empty;

if (user.Username != enteredUsername || user.Password != enteredPassword || string.IsNullOrWhiteSpace(enteredUsername) || string.IsNullOrWhiteSpace(enteredPassword))
{
    Console.WriteLine("Invalid credentials. Exiting.");
    return;
}

Console.WriteLine($"Welcome, {user.Username}!");

TMDbServiceAPI tmdb = new TMDbServiceAPI();

while (true)
{
    Console.Write("\nSearch for a movie (type 'exit' to quit): ");
    string input = Console.ReadLine() ?? string.Empty;

    if (input.ToLower() == "exit") break;
    if (string.IsNullOrWhiteSpace(input)) continue;

    MovieSearchResponse response = await tmdb.SearchMovie(input);

    if (response.Results.Count == 0)
    {
        Console.WriteLine("No movies found.");
        continue;
    }

    List<Movie> topResults = response.Results.Take(3).ToList();

    for (int i = 0; i < topResults.Count; i++)
    {
        Movie movie = topResults[i];
        Console.WriteLine($"  {i + 1}. {movie.Title} ({movie.ReleaseDate}) — TMDb: {movie.Rating:F1}/10");
    }

    Console.Write("\nEnter a number to rate a movie, or press Enter to keep searching: ");
    string pick = Console.ReadLine() ?? string.Empty;

    if (string.IsNullOrWhiteSpace(pick)) continue;

    if (!int.TryParse(pick, out int choice) || choice < 1 || choice > topResults.Count)
    {
        Console.WriteLine("Invalid selection.");
        continue;
    }

    Movie selected = topResults[choice - 1];

    Console.Write($"Your rating for \"{selected.Title}\" (1-10): ");
    string ratingInput = Console.ReadLine() ?? string.Empty;

    if (!double.TryParse(ratingInput, out double userRating) || userRating < 1 || userRating > 10)
    {
        Console.WriteLine("Invalid rating. Must be between 1 and 10.");
        continue;
    }

    user.Collection.Add(new RatedMovie(selected, userRating));
    Console.WriteLine($"Added \"{selected.Title}\" with your rating of {userRating:F1}/10 to your collection.");

    Console.Write("\nKeep searching (s) or view your collection (c)? ");
    string next = (Console.ReadLine() ?? string.Empty).Trim().ToLower();

    if (next == "c")
    {
        Console.WriteLine($"\nYour collection ({user.Collection.Count} movie(s)):\n");
        foreach (RatedMovie rated in user.Collection)
        {
            Console.WriteLine($"  {rated.Movie.Title} ({rated.Movie.ReleaseDate}) — Your Rating: {rated.UserRating:F1}/10 | TMDb: {rated.Movie.Rating:F1}/10");
        }
    }
}
