using ProgrammingLanguage.Application.Evaluating;
using ProgrammingLanguage.Shared.Helpers;

namespace ProgrammingLanguage.Application.Parsing;

internal class AssignmentNode(IdentifierNode identifier, Node value, Range<Position> range) : Node(range)
{
	public readonly IdentifierNode Identifier = identifier;
	public readonly Node Value = value;

	public override string ToString()
	{
		return $"({Identifier}: {Value})";
	}

	public override T Accept<T>(IAstVisitor<T> visitor, Scope location)
	{
		return visitor.Visit(location, this);
	}
}

