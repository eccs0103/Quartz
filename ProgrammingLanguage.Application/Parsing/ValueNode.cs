using ProgrammingLanguage.Application.Abstractions;
using ProgrammingLanguage.Shared.Helpers;

namespace ProgrammingLanguage.Application.Parsing;

internal class ValueNode(string type, object? value, Range<Position> range) : Node(range)
{
	public readonly string Type = type;
	public readonly object? Value = value;

	public override string ToString()
	{
		return $"{Value ?? "null"}";
	}

	public static ValueNode NullAt(Range<Position> range)
	{
		return new ValueNode("Number", null, range);
	}

	public override T Accept<T>(IResolverVisitor<T> visitor)
	{
		return visitor.Visit(this);
	}
}
