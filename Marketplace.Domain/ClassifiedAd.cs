namespace Marketplace.Domain;

public class ClassifiedAd
{
    public Guid Id { get; }
    private UserId _ownerId;

    public ClassifiedAd(Guid id, UserId ownerId)
    {
        Id = id; // validation check for ad id moved to valueobject ClassifiedAdId
        _ownerId = ownerId; // validation check for ownerId moved to valueobject UserId
    }

    public void SetTitle(string title) => _title = title;
    public void UpdateText(string text) => _text = text;
    public void UpdatePrice(decimal price) => _price = price;

    private string _title;
    private string _text;
    private decimal _price;
}