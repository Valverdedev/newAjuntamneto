namespace ChurchSuite.Shared.Domain.Common;

public abstract class DomainEvent
{
    public DateTime OccurredOnUtc { get; } = DateTime.UtcNow;
}
