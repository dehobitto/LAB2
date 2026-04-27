namespace AirlineApp.Models;

public class AirplaneFormViewModel
{
    public Airplane Airplane { get; set; } = new();
    public string Mode { get; set; } = "view"; // "view" | "create" | "edit"
    public List<Manufacturer> Manufacturers { get; set; } = [];
    public List<Airline> Airlines { get; set; } = [];
    public string? ErrorMessage { get; set; }
}
