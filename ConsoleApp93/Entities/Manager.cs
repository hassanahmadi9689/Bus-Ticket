using System.Globalization;
using System.Security.Cryptography;
using ConsoleApp93.EntityMap;

namespace ConsoleApp93.Entities;

public static class Manager
{
    private static readonly EFDataContext RelationShipDb = new();

    public static void AddBus(string name, int type)
    {
        switch (type)
        {
            case 1:
            {
                var normalBus = new NormalBus(name);
                RelationShipDb.NormalBuses.Add(normalBus);
                RelationShipDb.SaveChanges();
                break;
            }
            case 2:
            {
                var vipBus = new VIPBus(name);
                RelationShipDb.VIPBuses.Add(vipBus);
                RelationShipDb.SaveChanges();
                break;
            }
            default:
            {
                throw new Exception("YOU CHOOSE WRONG NUMBER ....");
            }
        }
    }

    public static void AddCity(string name)
    {
        var city = new City
        {
            Name = name
        };
        RelationShipDb.Cities.Add(city);
        RelationShipDb.SaveChanges();
    }

    public static void BusSelection(int busId, int typeBus)
    {
        switch (typeBus)
        {
            case 1:
            {
                var check =
                    RelationShipDb.NormalBuses.Any(_ => _.Id == busId);
                if (check is false)
                {
                    throw new Exception("Wrong Bus ID...");
                }

                break;
            }
            case 2:
            {
                var check =
                    RelationShipDb.VIPBuses.Any(_ => _.Id == busId);
                if (check is false)
                {
                    throw new Exception("Wrong Bus ID..");
                }

                break;
            }
        }
    }

    public static void DefinitionTrip(DateTime timeToLeave, decimal price,
        int busId, int originId, int destinationId)
    {
        var trip = new Trip
        {
            DestinationId = destinationId,
            OriginId = originId,
            BusId = busId,
            Price = price,
            TimeOfLeave = timeToLeave
        };
        RelationShipDb.Trips.Add(trip);
        RelationShipDb.SaveChanges();
    }

    public static void ShowCityList()
    {
        var cities = RelationShipDb.Cities.ToList();
        foreach (var city in cities)
        {
            Console.WriteLine($"Name: {city.Name} - ID:{city.Id}");
        }
    }

    public static void ShowBusByType(int typeBus)
    {
        switch (typeBus)
        {
            case 1:
            {
                var buses = RelationShipDb.NormalBuses.ToList();

                foreach (var bus in buses)
                {
                    Console.WriteLine(
                        $"Name : {bus.Name} -  Id: {bus.Id}");
                }

                break;
            }
            case 2:
            {
                var buses = RelationShipDb.VIPBuses.ToList();
                foreach (var bus in buses)
                {
                    Console.WriteLine(
                        $"Name : {bus.Name} -  Id: {bus.Id}");
                }

                break;
            }
        }
    }

    public static void TripListToShow()
    {
        var trip = RelationShipDb.Trips.ToList();
        foreach (var detail in trip)
        {
            var bus = RelationShipDb.Buses.Find(detail.BusId);
            if (bus == null)
            {
                throw new Exception("Bus Not Found;");
            }

            var originCity = RelationShipDb.Cities.Where(
                    _ => _.Id == detail.OriginId).Select(_ => _.Name)
                .SingleOrDefault();
            if (originCity == null)
            {
                throw new Exception("Origin City Not Found;");
            }

            var destinationCity = RelationShipDb.Cities
                .Where(_ => _.Id == detail.DestinationId)
                .Select(_ => _.Name)
                .SingleOrDefault();
            if (destinationCity == null)
            {
                throw new Exception("Destination City Not Found;");
            }

            Console.WriteLine(
                $"{detail.Id} - {bus.Name} - {bus.GetType().Name} - From:" +
                $"{originCity} -" +
                $"To: {destinationCity} - Ticket Price :" +
                $"{detail.Price}  ");
        }
    }

    public static void TakeTicket(int tripId, int status, int sit)
    {
        var trip =
            RelationShipDb.Trips.SingleOrDefault(_ => _.Id == tripId);
        if (trip == null)
        {
            throw new Exception("Trip Not Found;");
        }

        var bus =
            RelationShipDb.Buses.SingleOrDefault(_ => _.Id == trip.BusId);
        if (bus == null)
        {
            throw new Exception("Bus Not Found;");
        }

        if (sit > bus.Capacity)
        {
            throw new Exception("Choose Wrong Number");
        }

        if (RelationShipDb.TakeTickets.Any(_ =>
                _.SitNumb == sit && _.Status == TicketStatus.Sold &&
                _.TripId == tripId))
        {
            throw new Exception("Ticket is Sold");
        }

        if (RelationShipDb.TakeTickets.Any(_ =>
                _.SitNumb == sit && _.Status == TicketStatus.Reservation &&
                _.TripId == tripId))
        {
            throw new Exception("ticket is reserved");
        }


        switch (status)
        {
            case 1:
            {
                var takeTicket = new TakeTicket
                {
                    TripId = tripId,
                    SitNumb = sit,
                    Status = TicketStatus.Sold,
                    PaidAmount = trip.Price,
                    Income = trip.Price
                };
                RelationShipDb.TakeTickets.Add(takeTicket);
                RelationShipDb.SaveChanges();
                break;
            }
            case 2:
            {
                var takeTicket = new TakeTicket
                {
                    TripId = tripId,
                    SitNumb = sit,
                    Status = TicketStatus.Reservation
                };
                var a = trip.Price * (decimal)(0.3);
                takeTicket.PaidAmount = a;
                takeTicket.Income = a;
                RelationShipDb.TakeTickets.Add(takeTicket);
                RelationShipDb.SaveChanges();
                break;
            }
        }
    }

    public static void ShowSits(int tripId)
    {
        var trip =
            RelationShipDb.Trips.SingleOrDefault(_ => _.Id == tripId);
        if (trip == null)
        {
            throw new Exception("Trip Not Found;");
        }

        var bus =
            RelationShipDb.Buses.SingleOrDefault(_ => _.Id == trip.BusId);
        if (bus == null)
        {
            throw new Exception("Bus Not Found;");
        }

        var reservedSits = RelationShipDb.TakeTickets
            .Where(_ =>
                _.TripId == tripId && _.Status == TicketStatus.Reservation)
            .Select(_ => _.SitNumb)
            .ToList();

        var soldSits = RelationShipDb.TakeTickets
            .Where(_ =>
                _.TripId == tripId && _.Status == TicketStatus.Sold)
            .Select(_ => _.SitNumb)
            .ToList();
        MakeSitStyle(bus.Capacity, reservedSits, soldSits);
    }

    private static void MakeSitStyle(int capacity,
        ICollection<int> reservedSits,
        ICollection<int> soldSits)
    {
        switch (capacity)
        {
            case 30:
                for (int i = 1; i <= capacity; i++)
                {
                    var r = (double)i / 3 > 5 && (double)i / 3 <= 6;
                    if (!r && (i + 1) % 3 == 0)
                    {
                        Console.Write("     ");
                    }
                    else if (!r && i % 3 == 0)
                    {
                        Console.Write(" ");
                    }

                    Console.Write(reservedSits.Contains(i) ? "RR" :
                        soldSits.Contains(i) ? "SS" :
                        i.ToString().PadLeft(2, '0'));
                    if (r || i % 3 == 0)
                    {
                        Console.Write("\n");
                    }
                }

                break;
            case 44:
                for (int i = 1; i <= capacity; i++)
                {
                    var r = (double)i / 4 > 5 && (double)i / 4 <= 6;
                    if (!r && i % 2 == 1 && (i / 2) % 2 == 1)
                    {
                        Console.Write("     ");
                    }
                    else if (i % 2 == 0)
                    {
                        Console.Write(" ");
                    }

                    Console.Write(reservedSits.Contains(i) ? "RR" :
                        soldSits.Contains(i) ? "SS" :
                        i.ToString().PadLeft(2, '0'));
                    if ((r && i % 2 == 0) || i % 4 == 0)
                    {
                        Console.Write("\n");
                    }
                }

                break;
        }
    }

    public static void CanselTicket(int tripId, int sitNumber)
    {
        var trip =
            RelationShipDb.Trips.SingleOrDefault(_ => _.Id == tripId);
        if (trip == null)
        {
            throw new Exception("Trip Not Found;");
        }

        ShowSits(tripId);


        var ticket =
            RelationShipDb.TakeTickets.SingleOrDefault(_ =>
                _.TripId == trip.Id && _.SitNumb == sitNumber);

        if (ticket == null)
        {
            throw new Exception("Ticket Not Found;");
        }


        if (ticket.Status is TicketStatus.Reservation)
        {
            ticket.Status = TicketStatus.ResCancel;
            ticket.IsCanceled = true;
            var inCome = ticket.PaidAmount * (decimal)(0.2);
            ticket.Income = inCome;
        }

        if (ticket.Status is TicketStatus.Sold)
        {
            ticket.Status = TicketStatus.BuyCancel;
            ticket.IsCanceled = true;
            var inCome = ticket.PaidAmount * (decimal)(0.1);
            ticket.Income = inCome;
        }


        RelationShipDb.SaveChanges();
    }

    public static void TripReport(int tripId)
    {
        var trip =
            RelationShipDb.Trips.SingleOrDefault(_ => _.Id == tripId);
        if (trip == null)
        {
            throw new Exception("Trip Not Found ;");
        }

        var inCome = RelationShipDb.TakeTickets
            .Where(_ => _.TripId == trip.Id).Select(_ => _.Income).Sum()
            .ToString(CultureInfo.CurrentCulture);

        var bus =
            RelationShipDb.Buses.SingleOrDefault(_ => _.Id == trip.BusId);

        if (bus == null)
        {
            throw new Exception("Bus Not Found");
        }

        var capacity = bus.Capacity;

        var reservedAndSoldSeats =
            RelationShipDb.TakeTickets.Count(_ => _.TripId == trip.Id &&
                _.Status == TicketStatus.Reservation ||
                _.Status == TicketStatus.Sold);

        var emptySeats = capacity - reservedAndSoldSeats;
        var canceledReservedSeats = RelationShipDb.TakeTickets.Count(_ =>
            _.TripId == trip.Id && _.IsCanceled &&
            _.Status == TicketStatus.ResCancel);
        var canceledSoldSeats = RelationShipDb.TakeTickets.Count(_ =>
            _.TripId == trip.Id && _.IsCanceled &&
            _.Status == TicketStatus.BuyCancel);

        Console.WriteLine(
            $" Income :{inCome} - Count Of Empty Seats:{emptySeats} " +
            $"- CountCanceledSoldSeats : {canceledSoldSeats} -" +
            $" CountCanceledReservedSeats : {canceledReservedSeats}");
    }
}