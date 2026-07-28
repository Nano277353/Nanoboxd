# Nanoboxd

A [Letterboxd](https://letterboxd.com/)-inspired movie rating app for the terminal, built in **C# / .NET 10**. Search for movies through the [TMDb API](https://developer.themoviedb.org/docs), rate them on a 1–10 scale, and build your personal collection — all from the command line.

```
  _   _                   ____               _
 | \ | |                 |  _ \             | |
 |  \| | __ _ _ __   ___ | |_) | _____  ____| |
 | . ` |/ _` | '_ \ / _ \|  _ < / _ \ \/ / _` |
 | |\  | (_| | | | | (_) | |_) | (_) >  < (_| |
 |_| \_|\__,_|_| |_|\___/|____/ \___/_/\_\__,_|
```

## Features

- **Account creation & login** — create a username and password to start a session
- **Movie search** — query the TMDb REST API and get the top 3 matches with title, release date, and TMDb average rating
- **Personal ratings** — rate any movie from 1 to 10 and add it to your collection
- **Collection view** — see everything you've rated, with your score side by side with the TMDb community rating
- **JSON (de)serialization** — TMDb responses are mapped to strongly-typed models with `System.Text.Json`

## Tech Stack

- **C# 13 / .NET 10** — console application with implicit usings and nullable reference types enabled
- **HttpClient** — REST calls to the TMDb API
- **System.Text.Json** — JSON deserialization with `[JsonPropertyName]` attribute mapping

## Project Structure

```
Nanoboxd/
├── Nanoboxd.sln
└── Nanoboxd/
    ├── Program.cs                    # Entry point: login flow + search/rate loop
    ├── Header.txt                    # ASCII art banner
    ├── Classes/
    │   ├── User.cs                   # User credentials and movie collection
    │   ├── Movie.cs                  # Movie model mapped from the TMDb API
    │   ├── RatedMovie.cs             # A movie paired with the user's rating
    │   └── MovieSearchResponse.cs    # TMDb search response wrapper
    └── Services/
        ├── TMDBServiceAPI.cs         # TMDb REST API client
        └── UserStore.cs              # JSON persistence for user data
```

## Requisites

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- A free [TMDb API key](https://www.themoviedb.org/settings/api)

### Setup

1. Clone the repository:

   ```bash
   git clone https://github.com/Nano277353/Nanoboxd.git
   cd Nanoboxd
   ```

2. Set your TMDb API key as an environment variable (the app won't start without it):

   ```bash
   # Windows (PowerShell)
   $env:TMDB_API_KEY = "your_api_key_here"

   # macOS / Linux
   export TMDB_API_KEY="your_api_key_here"
   ```

3. Run the app:

   ```bash
   cd Nanoboxd
   dotnet run
   ```

## Usage

1. Create a username and password, then log in
2. Type a movie title to search TMDb
3. Pick one of the top 3 results and give it a rating from 1 to 10
4. Choose `s` to keep searching or `c` to view your collection
5. Type `exit` to quit

Example session:

```
Search for a movie (type 'exit' to quit): interstellar
  1. Interstellar (2014-11-05) — TMDB Rating: 8.5/10
  2. ...

Enter a number to rate a movie, or press Enter to keep searching: 1
Your rating for "Interstellar" (1-10): 9
Added "Interstellar" with your rating of 9.0/10 to your collection.
```

## Roadmap

- [✓] Persist user accounts and collections between sessions (via `UserStore`)
- [✓] Password hashing instead of plain-text credentials
- [ ] Edit and delete ratings
- [ ] More search results with pagination
- [ ] Actual front end interface

## Acknowledgments

This product uses the TMDb API but is not endorsed or certified by TMDb.

## Author

**Nano** — Computer Engineering student at U-ERRE (Monterrey, Mexico)
GitHub: [@Nano277353](https://github.com/Nano277353)
