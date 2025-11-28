using ChurchSuite.Shared.Domain.Common;

namespace ChurchSuite.Business.Domain.Billing;

public class Subscription : Entity, ITenantScopedAggregate
{
    public TenantId TenantId { get; init; }
    public Guid CustomerId { get; set; }
    public Guid PlanId { get; set; }
    public DateTime StartedOnUtc { get; set; } = DateTime.UtcNow;
    public DateTime? ExpiresOnUtc { get; set; }
    public bool Active { get; set; } = true;
}
