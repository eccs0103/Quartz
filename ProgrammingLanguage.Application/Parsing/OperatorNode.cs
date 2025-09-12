using ProgrammingLanguage.Shared.Helpers;

namespace ProgrammingLanguage.Application.Parsing;

internal abstract class OperatorNode(string @operator, Range<Position> range) : Node(range)
{
	public readonly string Operator = @operator;
}
