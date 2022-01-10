using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniRent.Application.Rentals.Commands.RentCar;

public class RentCarDto
{
    public Guid QuoteId { get; set; }
    public Guid RentId { get; set; }
    public DateTime RentAt { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int RentCompanyId { get; set; }
}
