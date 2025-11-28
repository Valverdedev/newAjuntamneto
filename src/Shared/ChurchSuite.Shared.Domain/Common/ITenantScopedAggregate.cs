namespace ChurchSuite.Shared.Domain.Common;

public interface ITenantScopedAggregate
{
    TenantId TenantId { get; }
}
