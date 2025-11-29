using ChurchSuite.Shared.Application.Common;
using ChurchSuite.Shared.Domain.Common;

namespace ChurchSuite.Church.Web.Tenancy;

public class TenantContext : ITenantContext
{
    public TenantId TenantId { get; set; } = TenantId.New();

    public string Code { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public TenantId CurrentTenant => TenantId;
}
