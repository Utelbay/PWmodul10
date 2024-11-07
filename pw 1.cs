using System;
using System.Collections.Generic;


public interface IHotelService
{
    string Execute(string action, params object[] parameters);
}


public class RoomBookingSystem : IHotelService
{
    public string Execute(string action, params object[] parameters)
    {
        switch (action)
        {
            case "BookRoom":
                return "";
            case "CancelBooking":
                return $"";
            default:
                return "";
        }
    }
}


public class RestaurantSystem : IHotelService
{
    public string Execute(string action, params object[] parameters)
    {
        switch (action)
        {
            case "BookTable":
                return "";
            case "OrderFood":
                return "";
            default:
                return "";
        }
    }
}

public class EventManagementSystem : IHotelService
{
    public string Execute(string action, params object[] parameters)
    {
        switch (action)
        {
            case "BookEventHall":
                return "";
            case "OrderEquipment":
                return "";
            default:
                return "";
        }
    }
}


public class CleaningService : IHotelService
{
    public string Execute(string action, params object[] parameters)
    {
        switch (action)
        {
            case "ScheduleCleaning":
                return "";
            case "RequestCleaning":
                return "";
            default:
                return "";
        }
    }
}


public class HotelFacade
{
    private readonly Dictionary<string, IHotelService> _services;

    public HotelFacade()
    {
        _services = new Dictionary<string, IHotelService>
        {
            { "Room", new RoomBookingSystem() },
            { "Restaurant", new RestaurantSystem() },
            { "Event", new EventManagementSystem() },
            { "Cleaning", new CleaningService() }
        };
    }

    public string BookRoomWithServices(string roomType, int nights, List<string> foodOrders = null, string cleaningTime = null)
    {
        var result = new List<string> { _services["Room"].Execute("BookRoom", roomType, nights) };
        if (foodOrders != null)
            result.Add(_services["Restaurant"].Execute("OrderFood", foodOrders));
        if (cleaningTime != null)
            result.Add(_services["Cleaning"].Execute("ScheduleCleaning", 1, cleaningTime));
        return string.Join("\n", result);
    }

    public string OrganizeEvent(int attendees, List<string> equipment, string roomType = null, int nights = 0)
    {
        var result = new List<string>
        {
            _services["Event"].Execute("BookEventHall", attendees),
            _services["Event"].Execute("OrderEquipment", equipment)
        };
        if (roomType != null && nights > 0)
            result.Add(_services["Room"].Execute("BookRoom", roomType, nights));
        return string.Join("\n", result);
    }

    public string BookTableWithTaxi(int persons, string time)
    {
        var result = new List<string> { _services["Restaurant"].Execute("BookTable", persons, time), "Такси вызвано для гостей." };
        return string.Join("\n", result);
    }

    public string CancelRoomBooking(int roomId) => _services["Room"].Execute("CancelBooking", roomId);

    public string RequestCleaning(int roomId) => _services["Cleaning"].Execute("RequestCleaning", roomId);
}


public class Program
{
    public static void Main()
    {
        var hotel = new HotelFacade();

        Console.WriteLine(hotel.BookRoomWithServices("люкс", 3, new List<string> { "салат", "паста" }, "10:00"));
        Console.WriteLine(hotel.OrganizeEvent(50, new List<string> { "проектор", "микрофон" }, "стандарт", 2));
        Console.WriteLine(hotel.BookTableWithTaxi(2, "19:00"));
        Console.WriteLine(hotel.CancelRoomBooking(101));
        Console.WriteLine(hotel.RequestCleaning(101));
    }
}
