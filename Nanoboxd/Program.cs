using Classes;
using Services;

string banner = File.ReadAllText("Header.txt");

Console.WriteLine(banner);

List<User> users = UserStore.LoadUsers();

User? currentUser = null;

while (currentUser == null)
{
    Console.Write("\n(L)ogin or (R)egister? ");
    string choice = (Console.ReadLine() ?? string.Empty).Trim().ToLower();

    if (choice == "r")
    {
        Console.Write("Choose a username: ");
        string username = (Console.ReadLine() ?? string.Empty).Trim();

        if (string.IsNullOrWhiteSpace(username))
        {
            Console.WriteLine("Username cannot be empty.");
            continue;
        }

        if (users.Any(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase)))
        {
            Console.WriteLine("That username is already taken.");
            continue;
        }

        Console.Write("Choose a password: ");
        string password = Console.ReadLine() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(password))
        {
            Console.WriteLine("Password cannot be empty.");
            continue;
        }

        User newUser = new User { Username = username };
        newUser.SetPassword(password);

        users.Add(newUser);
        UserStore.SaveUsers(users);

        Console.WriteLine($"Account created! Welcome, {newUser.Username}!");
        currentUser = newUser;
    }
    else if (choice == "l")
    {
        Console.Write("Username: ");
        string username = (Console.ReadLine() ?? string.Empty).Trim();

        Console.Write("Password: ");
        string password = Console.ReadLine() ?? string.Empty;

        User? match = users.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));

        if (match == null || !match.VerifyPassword(password))
        {
            Console.WriteLine("Invalid credentials.");
            continue;
        }

        Console.WriteLine($"Welcome back, {match.Username}!");
        currentUser = match;
    }
    else
    {
        Console.WriteLine("Please enter 'L' or 'R'.");
    }
}

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
        Console.WriteLine($"  {i + 1}. {movie.Title} ({movie.ReleaseDate}) — TMDB Rating: {movie.Rating:F1}/10");
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

    currentUser.Collection.Add(new RatedMovie(selected, userRating));
    UserStore.SaveUsers(users);
    Console.WriteLine($"Added \"{selected.Title}\" with your rating of {userRating:F1}/10 to your collection.");

    Console.Write("\nKeep searching (s) or view your collection (c)? ");
    string next = (Console.ReadLine() ?? string.Empty).Trim().ToLower();

    if (next == "c")
    {
        Console.WriteLine($"\nYour collection ({currentUser.Collection.Count} movie(s)):\n");
        foreach (RatedMovie rated in currentUser.Collection)
        {
            Console.WriteLine($"  {rated.Movie.Title} ({rated.Movie.ReleaseDate}) — Your Rating: {rated.UserRating:F1}/10 | TMDB Rating: {rated.Movie.Rating:F1}/10");
        }
    }
}
