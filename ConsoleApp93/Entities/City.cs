namespace ConsoleApp93.Entities;

public class City
{

    public int Id { get; set; }
    public string Name { get; set; }
    public HashSet<Trip> TripOrigins { get; set; }
    public HashSet<Trip> TripDestinations { get; set; }
}