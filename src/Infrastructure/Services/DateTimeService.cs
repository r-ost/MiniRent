using MiniRent.Application.Common.Interfaces;

namespace MiniRent.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}