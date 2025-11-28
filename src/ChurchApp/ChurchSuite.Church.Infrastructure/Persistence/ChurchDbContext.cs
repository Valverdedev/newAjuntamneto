using System.Linq.Expressions;
using ChurchSuite.Church.Domain.Organization;
using ChurchSuite.Shared.Application.Common;
using ChurchSuite.Shared.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace ChurchSuite.Church.Infrastructure.Persistence;

public class ChurchDbContext : DbContext
{
    private readonly ITenantContext _tenantContext;

    public ChurchDbContext(DbContextOptions<ChurchDbContext> options, ITenantContext tenantContext)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    public DbSet<Tenant> Tenants => Set<Tenant>();
    public DbSet<OrganizationalUnit> OrganizationalUnits => Set<OrganizationalUnit>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var tenantId = _tenantContext.CurrentTenant;
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(ITenantScopedAggregate).IsAssignableFrom(entityType.ClrType))
            {
                modelBuilder.Entity(entityType.ClrType).HasQueryFilter(CreateTenantFilter(entityType.ClrType, tenantId));
            }
        }
    }

    private static LambdaExpression CreateTenantFilter(Type clrType, TenantId tenantId)
    {
        var parameter = Expression.Parameter(clrType, "entity");
        var tenantProperty = Expression.Property(parameter, nameof(ITenantScopedAggregate.TenantId));
        var tenantValue = Expression.Constant(tenantId);
        var equality = Expression.Equal(tenantProperty, tenantValue);
        return Expression.Lambda(equality, parameter);
    }
}
