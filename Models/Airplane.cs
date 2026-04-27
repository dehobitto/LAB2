using System.ComponentModel.DataAnnotations;

namespace AirlineApp.Models;

public class Airplane
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Модель є обов'язковою")]
    [MaxLength(100)]
    public string Model { get; set; } = "";

    [Required(ErrorMessage = "Реєстраційний номер є обов'язковим")]
    [MaxLength(20)]
    public string RegistrationNumber { get; set; } = "";

    [Range(1, 1000, ErrorMessage = "Місткість має бути від 1 до 1000")]
    public int Capacity { get; set; }

    [Range(1900, 2100, ErrorMessage = "Рік має бути від 1900 до 2100")]
    public int YearManufactured { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Оберіть виробника")]
    public int ManufacturerId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Оберіть авіакомпанію")]
    public int AirlineId { get; set; }

    // populated by repository join queries
    public Manufacturer? Manufacturer { get; set; }
    public Airline? Airline { get; set; }
}
