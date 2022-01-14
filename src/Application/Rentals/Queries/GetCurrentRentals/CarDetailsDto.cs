using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniRent.Application.Rentals.Queries.GetCurrentRentals;

public class CarDetailsDto
{
    public string Brand { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public int EnginePower { get; set; }
    public string EnginePowerType { get; set; } = string.Empty;
    public int YearOfProduction { get; set; } 
    public string Description { get; set; } = string.Empty;
}
