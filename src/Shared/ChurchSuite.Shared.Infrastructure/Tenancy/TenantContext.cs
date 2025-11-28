using ChurchSuite.Shared.Application.Common;
using ChurchSuite.Shared.Domain.Common;

namespace ChurchSuite.Shared.Infrastructure.Tenancy;

public class TenantContext : ITenantContext
{
    public TenantContext(TenantId tenantId)
    {
        CurrentTenant = tenantId;
    }

    public TenantId CurrentTenant { get; }
}
