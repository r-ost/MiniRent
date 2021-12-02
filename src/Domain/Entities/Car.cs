using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Entities;

public class Car
{
    public int Id { get; set; }

    public int HorsePower { get; set; }

    public int YearOfProduction { get; set; }

    public string? Description { get; set; }

    public int CompanyId { get; set; }

    public CarModel CarModel { get; set; } = CarModel.Empty;

    public Company Company { get; set; } = null!;
}
