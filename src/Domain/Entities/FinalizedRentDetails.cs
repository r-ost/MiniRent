using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniRent.Domain.Entities;

public class FinalizedRentDetails
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public string OverallState { get; set; } = string.Empty;
    public int OdometerValueInKm { get; set; }
    public string PhotoPath { get; set; } = string.Empty;
    public string ProtocolPath { get; set; } = string.Empty;
    public int RentId { get; set; }
    public Rent Rent { get; set; } = null!;
}
