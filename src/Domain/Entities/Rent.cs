using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Entities;

public class Rent
{
    public int Id { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public RentStatus RentStatus { get; set; }

    public int CarId { get; set; }

    public int RenterId { get; set; }

    public Car Car { get; set; } = null!;

    public Renter Renter { get; set; } = null!;

    //TODO details, doku blob, note?
    //•	Rent company worker can show the history of all rented car
    //•	Every history item should have gone into the details button
    //•	In detail shouldn't be data when it was returned, a note from Rental Worker, documents attached

}
