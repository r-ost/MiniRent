using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniRent.Application.Rentals.Queries.GetCurrentRentals;

namespace MiniRent.Application.Rentals.Queries.GetCurrentRentalsWorker;

public class CurrentRentalWorkerDto
{
    public Guid RentId { get; set; }
    public CarDetailsDto CarDetailsDto { get; set; } = new CarDetailsDto();
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    public string RentCompanyName { get; set; } = string.Empty;
    public decimal TotalPrice { get; set; }
    public string Currency { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
}
