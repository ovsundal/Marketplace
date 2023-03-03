namespace Marketplace.Domain;

public record Money
{
    // factories
    public static Money FromDecimal(decimal amount) => new Money(amount);
    public static Money FromString(string amount) => new Money(decimal.Parse(amount));

    protected Money(decimal amount)
    {
        if (decimal.Round(amount, 2) != amount)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount cannot have more than 2 decimal places");
        }

        Amount = amount;
    }

    public decimal Amount { get; }

    public Money Add(Money summand) => new Money(Amount + summand.Amount);
    public Money Subtract(Money subtrahend) => new Money(Amount - subtrahend.Amount);
    public static Money operator +(Money summand1, Money summand2) => summand1.Add(summand2);
    public static Money operator -(Money minuend, Money subtrahend) => minuend.Subtract(subtrahend);
}
