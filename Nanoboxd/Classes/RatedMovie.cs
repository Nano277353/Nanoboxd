namespace Classes;

public class RatedMovie
{
    public Movie Movie { get; set; }
    public double UserRating { get; set; }

    public RatedMovie(Movie movie, double userRating)
    {
        Movie = movie;
        UserRating = userRating;
    }
}
