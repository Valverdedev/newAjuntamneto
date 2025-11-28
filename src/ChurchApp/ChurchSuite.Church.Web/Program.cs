using ChurchSuite.Church.Application;
using ChurchSuite.Church.Infrastructure.Persistence;
using ChurchSuite.Shared.Application.Common;
using ChurchSuite.Shared.Domain.Common;
using ChurchSuite.Shared.Infrastructure.Tenancy;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<ITenantContext>(sp =>
{
    var httpContext = sp.GetRequiredService<IHttpContextAccessor>().HttpContext;
    if (httpContext?.Items.TryGetValue(nameof(ITenantContext), out var value) == true && value is ITenantContext existing)
    {
        return existing;
    }

    return new TenantContext(TenantId.New());
});

builder.Services.AddDbContext<ChurchDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AssemblyReference).Assembly));

var app = builder.Build();

app.UseMiddleware<TenantResolutionMiddleware>();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
