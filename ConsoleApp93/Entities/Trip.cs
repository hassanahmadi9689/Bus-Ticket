namespace ConsoleApp93.Entities;

public class Trip
{

   // public Trip(DateTime timeOfLeave, decimal price,
      //  int BusId, int originId, int distinationID)
   // {
      //  TimeOfLeave = timeOfLeave;
     //   Price = price;
     //   OriginId = originId;
        //DestinationId = distinationID;

   // }
    public int Id { get; set; }
    public DateTime TimeOfLeave { get; set; }
    public decimal Price { get; set; }
    public int BusId { get; set; }
    public Bus Bus { get; set; }
    
    public int OriginId { get; set; }
    public City OriginCity { get; set; }
    public int DestinationId { get; set; }
    public City DestinationCity { get; set; }
    public HashSet<TakeTicket> TakeTickets { get; set; }

   
    
    
}