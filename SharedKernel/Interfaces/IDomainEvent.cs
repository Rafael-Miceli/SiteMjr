using System;

namespace SharedKernel.Interfaces
{
    public interface IDomainEvent
    {
        DateTime DateTimeEventOccurred { get; }
    }
}