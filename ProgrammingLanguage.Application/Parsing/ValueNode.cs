using ProgrammingLanguage.Application.Abstractions;
using ProgrammingLanguage.Shared.Helpers;

namespace ProgrammingLanguage.Application.Parsing;

internal class ValueNode(object? value, Range<Position> range) : Node(range)
{
	public readonly object? Value = value;

	public override string ToString()
	{
		return $"{Value ?? "null"}";
	}

	public T ValueAs<T>()
	{
		if (Value is T result) return result;
		string type = Value?.GetType().Name ?? "Null";
		throw new InvalidCastException($"Unable to convert '{Value}' from {type} to {typeof(T).Name}");
	}

	public static ValueNode NullAt(Range<Position> range)
	{
		return new ValueNode(null, range);
	}

	public override T Accept<T>(IResolverVisitor<T> visitor)
	{
		return visitor.Visit(this);
	}
}
