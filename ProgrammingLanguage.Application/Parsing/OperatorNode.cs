using ProgrammingLanguage.Shared.Helpers;

namespace ProgrammingLanguage.Application.Parsing;

internal abstract class OperatorNode(IdentifierNode @operator, Range<Position> range) : Node(range)
{
	public readonly IdentifierNode Operator = @operator;
}
