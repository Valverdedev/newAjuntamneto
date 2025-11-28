using ChurchSuite.Shared.Domain.Common;

namespace ChurchSuite.Business.Domain.Billing;

public class Customer : Entity
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public List<Subscription> Subscriptions { get; set; } = new();
}
