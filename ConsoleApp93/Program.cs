// See https://aka.ms/new-console-template for more information

using ConsoleApp93.Entities;

var Exit = false;
while (!Exit)
{
    try
    {
        Run();
    }
    catch (Exception exception)
    {
        ShowError(exception.Message);
    }
}

void Run()
{
    var option = GetNumberFromUser("Choose option:\n" +
                                   "1.Add Bus To Terminal\n" +
                                   "2.Definition Trip\n" +
                                   "3.Preview Trips\n" +
                                   "4.Take Ticket\n" +
                                   "5.Cancel The Ticket\n" +
                                   "6.Trip Report\n" +
                                   "7.Add City To CityTable\n" +
                                   "0.Exit");
    switch (option)
    {
        case 1:
        {
            var busName = GetValidStringFromUser("Enter The" +
                                                 " Name Of The Bus : ...");

            var type = GetNumberFromUser("Determine The Type " +
                                         "Of Bus Bringing In a Number : \n" +
                                         "1.Normal Bus\n" +
                                         "2.VIP Bus ");
            Manager.AddBus(busName, type);
            break;
        }
        case 2:
        {
            var busType = GetNumberFromUser(
                "Determine The Type Of Bus Bringing In a Number :\n" +
                "1.Normal Buses\n" +
                "2.VIP Buses");
            Manager.ShowBusByType(busType);
            var busId = GetNumberFromUser("Enter Bus ID : ...");
            Manager.BusSelection(busId, busType);
            Manager.ShowCityList();
            var origenId = GetNumberFromUser("Enter Origen Id :");
            var destinationId =
                GetNumberFromUser("Enter Destination Id :");
            var timeToLeave =
                GetDateFromUser(
                    "Enter Date To Leave :(example:2024/01/02 14:20:00)  ");
            var price = GetNumberFromUser("Enter Ticket Price :");
            Manager.DefinitionTrip(timeToLeave, price, busId, origenId,
                destinationId);

            break;
        }
        case 3:
        {
            Manager.TripListToShow();
            break;
        }
        case 4:
        {
            Manager.TripListToShow();
            var tripId =
                GetNumberFromUser("Enter the id of trip from the list :");

            Manager.ShowSits(tripId);

            var sitNumb =
                GetNumberFromUser("Choose sit number from the list :");
            var status = GetNumberFromUser("1.Buy\n" +
                                           "2.Reserve");
            Manager.TakeTicket(tripId, status, sitNumb);
            break;
        }
        case 5:
        {
            Manager.TripListToShow();
            var tripId = GetNumberFromUser("Select The Trip Id ...");
            Manager.ShowSits(tripId);
            var sitNumb =
                GetNumberFromUser("Enter The Sit Number To Cancel :");
            Manager.CanselTicket(tripId, sitNumb);
            break;
        }
        case 6:
        {
            Manager.TripListToShow();
            var tripId = GetNumberFromUser("Enter Trip Id :");
            Manager.TripReport(tripId);
            break;
        }
        case 7:
        {
            var cityName = GetValidStringFromUser("Enter Name" +
                "Of City :");
            Manager.AddCity(cityName);
            break;
        }
        case 0:
        {
            Exit = true;
            break;
        }
        default:
        {
            throw new Exception("Choose Wrong Option ...");
        }
    }
}


static string GetValidStringFromUser(string message)
{
    string? value;
    do
    {
        Console.WriteLine(message);
        value = Console.ReadLine();
    } while (string.IsNullOrWhiteSpace(value));

    return value;
}

static int GetNumberFromUser(string message)
{
    bool resultTryParseFirstNumber;
    int number;
    do
    {
        Console.WriteLine(message);
        resultTryParseFirstNumber =
            int.TryParse(Console.ReadLine(), out number);
    } while (!resultTryParseFirstNumber);

    return number;
}

static DateTime GetDateFromUser(string message)
{
    bool resultTryParseFirstNumber;
    DateTime dateTime;
    do
    {
        Console.WriteLine(message);
        resultTryParseFirstNumber =
            DateTime.TryParse(Console.ReadLine(), out dateTime);
    } while (!resultTryParseFirstNumber);

    return dateTime;
}


static void ShowError(string error)
{
    Console.WriteLine("*******************");
    Console.WriteLine(error);
    Console.WriteLine("*******************");
}