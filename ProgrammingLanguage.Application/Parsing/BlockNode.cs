using ProgrammingLanguage.Application.Evaluating;
using ProgrammingLanguage.Shared.Helpers;

namespace ProgrammingLanguage.Application.Parsing;

internal class BlockNode(IEnumerable<Node> statements, Range<Position> range) : Node(range)
{
	public readonly IEnumerable<Node> Statements = statements;

	public override string ToString()
	{
		return string.Join('\n', ["{", .. Statements.Select(node => node.ToString()), "}"]);
	}

	public override T Accept<T>(IAstVisitor<T> visitor, Scope location)
	{
		return visitor.Visit(location, this);
	}
}
