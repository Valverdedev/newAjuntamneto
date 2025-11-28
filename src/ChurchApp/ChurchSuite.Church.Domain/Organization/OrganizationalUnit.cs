using ChurchSuite.Shared.Domain.Common;

namespace ChurchSuite.Church.Domain.Organization;

public class OrganizationalUnit : Entity, ITenantScopedAggregate
{
    public TenantId TenantId { get; init; }
    public string Name { get; set; } = string.Empty;
    public OrganizationalUnitType Type { get; set; }
    public Guid? ParentId { get; set; }
}
