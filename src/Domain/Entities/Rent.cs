using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniRent.Domain.Enums;

namespace MiniRent.Domain.Entities;

public class Rent
{
    public int Id { get; set; }
    public string RentId { get; set; } = "";

    public DateTime? RentAt { get; set; }
    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }
    public decimal TotalPrice { get; set; }
    public string Currency { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;

    public RentStatus RentStatus { get; set; }

    public string CarId { get; set; } = "";

    public int RenterId { get; set; }

    public Renter Renter { get; set; } = null!;

    public int CompanyId { get; set; }

    public Company Company { get; set; } = null!;

    //TODO details, doku blob, note?
    //•	Rent company worker can show the history of all rented car
    //•	Every history item should have gone into the details button
    //•	In detail shouldn't be data when it was returned, a note from Rental Worker, documents attached

}
