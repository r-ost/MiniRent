using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniRent.Application.Rentals.Queries.GetCurrentRentals;

namespace MiniRent.Application.Rentals.Queries.GetRentDetails;

public class RentDetailsDto
{
    public Guid RentId { get; set; }
    public CarDetailsDto CarDetailsDto { get; set; } = new CarDetailsDto();
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    public string Location { get; set; } = string.Empty;
    public string RentCompanyName { get; set; } = string.Empty;
    public decimal TotalPrice { get; set; }
    public string Currency { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string OverallState { get; set; } = string.Empty;
    public int OdometerValueInKm { get; set; }
    public string PhotoPath { get; set; } = string.Empty;
    public string ProtocolPath { get; set; } = string.Empty;
}
