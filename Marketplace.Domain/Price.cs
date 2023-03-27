namespace Marketplace.Domain;

public record Price : Money
{
    public Price(Money amount) : base(amount)
    {
        // if (amount < 0)
        // {
        //     throw new ArgumentException("Price cannot be negative", nameof(amount));
        // }
    }
}
