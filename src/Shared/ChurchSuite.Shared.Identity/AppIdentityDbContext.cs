using System;
using ChurchSuite.Shared.Domain.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChurchSuite.Shared.Identity;

public class AppIdentityDbContext
    : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
{
    public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ApplicationUser>(b =>
        {
            b.ToTable("users");
            b.Property(x => x.DisplayName)
                .HasMaxLength(200);

            b.Property<TenantId?>("TenantId")
                .HasConversion(
                    v => v.HasValue ? v.Value.Value : (Guid?)null,
                    v => v.HasValue ? new TenantId(v.Value) : (TenantId?)null)
                .HasColumnName("tenant_id");

            b.Property(x => x.PersonId)
                .HasColumnName("person_id");

            b.Property(x => x.IsPlatformAdmin)
                .HasColumnName("is_platform_admin");
        });

        builder.Entity<ApplicationRole>(b =>
        {
            b.ToTable("roles");
            b.Property(x => x.Description)
                .HasMaxLength(256);
        });

        builder.Entity<IdentityUserRole<Guid>>().ToTable("user_roles");
        builder.Entity<IdentityUserClaim<Guid>>().ToTable("user_claims");
        builder.Entity<IdentityUserLogin<Guid>>().ToTable("user_logins");
        builder.Entity<IdentityRoleClaim<Guid>>().ToTable("role_claims");
        builder.Entity<IdentityUserToken<Guid>>().ToTable("user_tokens");
    }
}
