using MiniRent.Domain.Common;

namespace MiniRent.Application.Common.Interfaces
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}