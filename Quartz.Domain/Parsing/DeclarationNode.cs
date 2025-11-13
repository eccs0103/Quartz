using Quartz.Domain.Evaluating;
using Quartz.Shared.Helpers;

namespace Quartz.Domain.Parsing;

public class DeclarationNode(IdentifierNode type, IdentifierNode identifier, Node value, Range<Position> range) : Node(range)
{
	public readonly IdentifierNode Type = type;
	public readonly IdentifierNode Identifier = identifier;
	public readonly Node Value = value;

	public override string ToString()
	{
		return $"({Identifier} {Type}: {Value})";
	}

	public override T Accept<T>(IAstVisitor<T> visitor, Scope location)
	{
		return visitor.Visit(location, this);
	}
}
