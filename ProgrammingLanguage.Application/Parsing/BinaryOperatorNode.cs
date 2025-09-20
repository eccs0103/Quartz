using ProgrammingLanguage.Application.Abstractions;
using ProgrammingLanguage.Shared.Helpers;

namespace ProgrammingLanguage.Application.Parsing;

internal class BinaryOperatorNode(IdentifierNode @operator, Node left, Node right, Range<Position> range) : OperatorNode(@operator, range)
{
	public readonly Node Left = left;
	public readonly Node Right = right;

	public override string ToString()
	{
		return $"({Left} {Operator} {Right})";
	}

	public override T Accept<T>(IResolverVisitor<T> visitor)
	{
		return visitor.Visit(this);
	}
}
