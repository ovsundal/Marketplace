using System.Security.AccessControl;

namespace Marketplace.Domain;

public class ClassifiedAdText
{
    public string Value { get; }
    internal ClassifiedAdText(string text) => Value = text;

    public static ClassifiedAdText FromString(string text) => new ClassifiedAdText(text);

    public static implicit operator string(ClassifiedAdText text) => text.Value;
}