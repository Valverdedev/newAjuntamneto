using System;
using ChurchSuite.Shared.Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace ChurchSuite.Shared.Identity;

public class ApplicationUser : IdentityUser<Guid>
{
    public TenantId? TenantId { get; set; }

    public Guid? PersonId { get; set; }

    public string? DisplayName { get; set; }

    public bool IsPlatformAdmin { get; set; }
}
