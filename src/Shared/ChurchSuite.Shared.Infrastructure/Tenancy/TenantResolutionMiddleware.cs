using System.Linq;
using ChurchSuite.Shared.Application.Common;
using ChurchSuite.Shared.Domain.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace ChurchSuite.Shared.Infrastructure.Tenancy;

public class TenantResolutionMiddleware
{
    private readonly RequestDelegate _next;

    public TenantResolutionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var tenantIdHeader = context.Request.Headers["X-Tenant-Id"].FirstOrDefault();
        var tenantId = Guid.TryParse(tenantIdHeader, out var parsed)
            ? new TenantId(parsed)
            : TenantId.New();

        var tenantContext = ActivatorUtilities.CreateInstance<TenantContext>(context.RequestServices, tenantId);
        context.Items[nameof(ITenantContext)] = tenantContext;
        await _next(context);
    }
}
