using MiniRent.Domain.Common;
using MiniRent.Domain.Entities;

namespace MiniRent.Domain.Events;

public class TodoItemCreatedEvent : DomainEvent
{
    public TodoItemCreatedEvent(TodoItem item)
    {
        Item = item;
    }

    public TodoItem Item { get; }
}
