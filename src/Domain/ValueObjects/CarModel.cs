using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniRent.Domain.Common;

namespace MiniRent.Domain.ValueObjects;

public class CarModel : ValueObject
{
    public string Brand { get; private set; } = "";

    public string Model { get; private set; } = "";

    public static CarModel Empty => new();

    public CarModel()
    {

    }

    public CarModel(string brand, string model)
    {
        Brand = brand;
        Model = model;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Brand;
        yield return Model;
    }
}
