using MiniRent.Domain.Common;
using MiniRent.Domain.Entities;

namespace MiniRent.Domain.Events
{
    public class TodoItemCompletedEvent : DomainEvent
    {
        public TodoItemCompletedEvent(TodoItem item)
        {
            Item = item;
        }

        public TodoItem Item { get; }
    }
}