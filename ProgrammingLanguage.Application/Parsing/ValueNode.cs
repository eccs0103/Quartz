using ProgrammingLanguage.Application.Evaluating;
using ProgrammingLanguage.Shared.Helpers;

namespace ProgrammingLanguage.Application.Parsing;

internal class ValueNode(string tag, object? value, Range<Position> range) : Node(range)
{
	public readonly string Tag = tag;
	public readonly object? Value = value;

	public override string ToString()
	{
		return $"{Value ?? "null"}";
	}

	public static ValueNode NullAt(Range<Position> range)
	{
		return new ValueNode("Null", null, range);
	}

	public static ValueNode NullableAt(string tag, Range<Position> range)
	{
		return new ValueNode(tag, null, range);
	}

	public override T Accept<T>(IAstVisitor<T> visitor, Scope location)
	{
		return visitor.Visit(location, this);
	}
}
