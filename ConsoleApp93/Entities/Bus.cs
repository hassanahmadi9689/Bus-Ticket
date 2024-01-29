namespace ConsoleApp93.Entities;

public abstract class Bus
{
    protected Bus( string name)
    {
        Name = name;
    }
    public int Id { get; set; }
    public string Name { get; set; }
    public int Capacity { get; set; }
    public HashSet<Trip> Trips { get; set; }
    
}
