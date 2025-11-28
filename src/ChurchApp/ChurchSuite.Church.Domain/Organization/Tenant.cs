using ChurchSuite.Shared.Domain.Common;

namespace ChurchSuite.Church.Domain.Organization;

public class Tenant : Entity, ITenantScopedAggregate
{
    public TenantId TenantId { get; init; }
    public string Name { get; set; } = string.Empty;
    public string? Slug { get; set; }
}
