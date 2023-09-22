using System.Text.RegularExpressions;
using Marketplace.Framework;

namespace Marketplace.Domain;

public class ClassifiedAdTitle : Value<ClassifiedAdTitle>
{
    public static ClassifiedAdTitle FromString(string title)
    {
        CheckValidity(title);
        return new ClassifiedAdTitle(title);
    }

    // satisfy the serialization requirements
    protected ClassifiedAdTitle() { }

    public static ClassifiedAdTitle FromHtml(string htmlTitle) // factory function that converts HTML to the value object instance
    {
        var supportedTagsReplaced = htmlTitle
            .Replace("<i>", "*")
            .Replace("</i>", "*")
            .Replace("<b>", "**")
            .Replace("</b>", "**");

        var value = Regex.Replace(supportedTagsReplaced, "<.*?>", string.Empty);
        CheckValidity(value);

        return new ClassifiedAdTitle(value);
    }
    public string Value { get; }
    internal ClassifiedAdTitle(string value) => Value = value;
    public static implicit operator string(ClassifiedAdTitle title) => title.Value;

    private static void CheckValidity(string value)
    {
        if (value.Length > 100)
        {
            throw new ArgumentException("Title cannot be longer than 100 characters", nameof(value));
        }
    }
}