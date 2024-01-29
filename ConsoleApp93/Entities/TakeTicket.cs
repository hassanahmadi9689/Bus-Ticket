namespace ConsoleApp93.Entities;

public class TakeTicket
{
    public int Id { get; set; }
    public TicketStatus Status { get; set; }
    public bool IsCanceled { get; set; } = false;
    public int SitNumb { get; set; }
    public decimal Income { get; set; }
    public decimal PaidAmount { get; set; }
    public int TripId { get; set; }
    public Trip Trip { get; set; }
}