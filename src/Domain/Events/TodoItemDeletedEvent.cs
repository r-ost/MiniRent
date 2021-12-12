using MiniRent.Domain.Common;
using MiniRent.Domain.Entities;

namespace MiniRent.Domain.Events;

public class TodoItemDeletedEvent : DomainEvent
{
    public TodoItemDeletedEvent(TodoItem item)
    {
        Item = item;
    }

    public TodoItem Item { get; }
}
