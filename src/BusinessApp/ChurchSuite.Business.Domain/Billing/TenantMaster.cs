using ChurchSuite.Shared.Domain.Common;

namespace ChurchSuite.Business.Domain.Billing;

public class TenantMaster : Entity
{
    public TenantId TenantId { get; init; }
    public string DisplayName { get; set; } = string.Empty;
    public Guid CustomerId { get; set; }
    public Guid SubscriptionId { get; set; }
}
