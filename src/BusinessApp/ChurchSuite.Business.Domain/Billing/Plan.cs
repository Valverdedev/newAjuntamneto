using ChurchSuite.Shared.Domain.Common;

namespace ChurchSuite.Business.Domain.Billing;

public class Plan : Entity
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Currency { get; set; } = "USD";
    public int BillingCycleInDays { get; set; } = 30;
}
