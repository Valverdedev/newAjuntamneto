using ChurchSuite.Shared.Domain.Common;

namespace ChurchSuite.Shared.Application.Common;

public interface ITenantContext
{
    TenantId CurrentTenant { get; }
}
