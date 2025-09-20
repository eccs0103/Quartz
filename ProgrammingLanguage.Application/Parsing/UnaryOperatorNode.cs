using ProgrammingLanguage.Application.Abstractions;
using ProgrammingLanguage.Shared.Helpers;

namespace ProgrammingLanguage.Application.Parsing;

internal class UnaryOperatorNode(IdentifierNode @operator, Node target, Range<Position> range) : OperatorNode(@operator, range)
{
	public readonly Node Target = target;

	public override string ToString()
	{
		return $"({Operator} {Target})";
	}

	public override T Accept<T>(IResolverVisitor<T> visitor)
	{
		return visitor.Visit(this);
	}
}
