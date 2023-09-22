using System.Security.AccessControl;
using Marketplace.Framework;

namespace Marketplace.Domain;

public class ClassifiedAdText : Value<ClassifiedAdText>
{
    public string Value { get; }
    internal ClassifiedAdText(string text) => Value = text;

    public static ClassifiedAdText FromString(string text) => new ClassifiedAdText(text);

    // satisfy the serialization requirements
    protected ClassifiedAdText() { }

    public static implicit operator string(ClassifiedAdText text) => text.Value;
}