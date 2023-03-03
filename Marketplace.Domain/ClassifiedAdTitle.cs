using System.Text.RegularExpressions;

namespace Marketplace.Domain;

public record ClassifiedAdTitle
{
    public static ClassifiedAdTitle FromString(string title) => new ClassifiedAdTitle(title); // factory function that converts string to the value object instance

    public static ClassifiedAdTitle FromHtml(string htmlTitle) // factory function that converts HTML to the value object instance
    {
        var supportedTagsReplaced = htmlTitle
            .Replace("<i>", "*")
            .Replace("</i>", "*")
            .Replace("<b>", "**")
            .Replace("</b>", "**");

        return new ClassifiedAdTitle(Regex.Replace(supportedTagsReplaced, "<.*?>", string.Empty));
    }
    private readonly string _value;

    private ClassifiedAdTitle(string value)
    {
        if (value.Length > 100)
        {
            throw new ArgumentException("Title cannot be longer than 100 characters", nameof(value));
        }

        _value = value;
    }
}
