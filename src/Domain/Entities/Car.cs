using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniRent.Domain.ValueObjects;

namespace MiniRent.Domain.Entities;

public class Car
{
    public int Id { get; set; }

    public int HorsePower { get; set; }

    public int YearOfProduction { get; set; }

    public string Description { get; set; } = string.Empty;

    public int CompanyId { get; set; }

    public CarModel CarModel { get; set; } = CarModel.Empty;

    public Company Company { get; set; } = null!;
}
