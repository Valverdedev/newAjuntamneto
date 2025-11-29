using System.Linq;
using ChurchSuite.Shared.Application.Common;
using ChurchSuite.Shared.Domain.Common;
using ChurchSuite.Shared.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace ChurchSuite.Church.Web.Tenancy;

public class IdentityTenantResolutionMiddleware
{
    private readonly RequestDelegate _next;

    public IdentityTenantResolutionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ITenantContext tenantContext, UserManager<ApplicationUser> userManager)
    {
        TenantId? tenantId = null;

        if (context.User?.Identity?.IsAuthenticated == true)
        {
            var user = await userManager.GetUserAsync(context.User);
            tenantId = user?.TenantId;
        }

        tenantId ??= TryResolveFromHeader(context);

        if (tenantId is not null)
        {
            tenantContext.TenantId = tenantId.Value;
        }

        await _next(context);
    }

    private static TenantId? TryResolveFromHeader(HttpContext context)
    {
        var tenantIdHeader = context.Request.Headers["X-Tenant-Id"].FirstOrDefault();
        return Guid.TryParse(tenantIdHeader, out var parsed)
            ? new TenantId(parsed)
            : null;
    }
}
