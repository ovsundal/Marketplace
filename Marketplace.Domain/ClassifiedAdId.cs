namespace Marketplace.Domain;

public record ClassifiedAdId
{
    private readonly Guid _value;

    public ClassifiedAdId(Guid value)
    {
        if (value == default)
        {
            throw new ArgumentException(nameof(value), "Classified Ad id cannot be empty");
        }

        _value = value;
    }
}
